using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SammyController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private SammyControllerData data;

    private Vector2 currentInput, prevInput;
    private int currentJumps;
    private Vector3 velocity = Vector3.zero;

    private void Update() {
        GetInput();

        ResetVelocity();
        ApplyHorizontalSpeed();
        ApplyVerticalSpeed();
        ApplyGravity();

        controller.Move(velocity * Time.deltaTime);
        ApplyLogic();
    }

    private void ResetVelocity() {
        // We reset the velocity axes separately. Vertical should be cumulative, horizontal should be snappier.
        velocity.x = 0.0f;

        if (controller.isGrounded && velocity.y < -0.1f) {
            velocity.y = -0.1f;  // Need *SOME* downward input to get `controller.isGrounded` working.
        }
    }

    private void GetInput() {
        prevInput = currentInput;
        currentInput.x = Input.GetAxis("Horizontal");
        currentInput.y = Input.GetAxisRaw("Vertical");  // Binary inputs for up / down.
    }

    private void ApplyLogic() {
        if (controller.isGrounded) {
            currentJumps = 0;
        }
    }

    private void ApplyGravity() {
        velocity.y += GlobalVariables.instance.data.gravity * Time.deltaTime;
    }

    private void ApplyHorizontalSpeed() {
        float horizSpeed = currentInput.x * (controller.isGrounded ? data.horizontalSpeed_Grounded : data.horizontalSpeed_Air);
        velocity.x += horizSpeed;
    }

    private void ApplyVerticalSpeed() {
        if ((currentInput.y > 0 && prevInput.y <= 0) || Input.GetButtonDown("Jump")) {
            if (currentJumps < data.jumpCount) {
                velocity.y = Mathf.Sqrt(data.jumpHeight * -2.0f * GlobalVariables.instance.data.gravity);
                currentJumps++;
            }
        }
    }
}

[CreateAssetMenu(fileName="SammyControllerData", menuName="Data/SammyControllerData")]
public class SammyControllerData : ScriptableObject {
    [Header("Ground Movement")]
    public float horizontalSpeed_Grounded;
    
    [Header("Air Movement")]
    public float horizontalSpeed_Air;
    public float jumpHeight;
    public float jumpCount;
}
