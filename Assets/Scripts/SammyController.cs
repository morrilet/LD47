using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SammyController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private SammyControllerData data;

    private Vector2 currentInput, prevInput;
    private int currentJumps;
    private Vector3 velocity;

    private void Update() {
        GetInput();

        ResetVelocity(ref velocity);
        ApplyHorizontalSpeed(ref velocity);
        ApplyVerticalSpeed(ref velocity);
        ApplyGravity(ref velocity);
        ApplyTimescale(ref velocity);

        controller.Move(velocity);
        CheckIsGrounded();

        ApplyLogic();
    }

    private void ResetVelocity(ref Vector3 velocity) {
        // We reset the velocity axes separately. Vertical should be cumulative, horizontal should be snappier.
        velocity.x = 0.0f;

        if (controller.isGrounded && velocity.y < -0.1f) {
            Debug.Log("HERE");
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

    private void ApplyGravity(ref Vector3 velocity) {
        velocity += (Vector3.up * GlobalVariables.instance.data.gravity * Time.deltaTime);
    }

    private void ApplyHorizontalSpeed(ref Vector3 velocity) {
        float horizSpeed = currentInput.x * (controller.isGrounded ? data.horizontalSpeed_Grounded : data.horizontalSpeed_Air);
        velocity += Vector3.right * horizSpeed;
    }

    private void ApplyVerticalSpeed(ref Vector3 velocity) {
        if ((currentInput.y > 0 && prevInput.y <= 0) || Input.GetButtonDown("Jump")) {
            if (currentJumps < data.jumpCount) {
                velocity.y += Mathf.Sqrt(data.jumpHeight * -3.0f * GlobalVariables.instance.data.gravity);
                currentJumps++;
            }
        }
    }

    private void ApplyTimescale(ref Vector3 velocity) {
        velocity *= Time.deltaTime;
    }

    private void CheckIsGrounded() {
        RaycastHit hitInfo;
        bool isGrounded = Physics.SphereCast(
            transform.position,
            controller.radius,
            Vector3.down,
            out hitInfo,
            (controller.height / 2.0f + controller.skinWidth),
            data.groundCheckLayerMask
        );

        Debug.Log(isGrounded);
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

    public LayerMask groundCheckLayerMask;
}
