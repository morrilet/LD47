using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SammyController : MonoBehaviour, ITrampolineTarget, IConveyorBeltTarget
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private SammyControllerData data;
    [SerializeField] private Pusher pusher;

    public bool isGrounded { get; private set; }
    public bool isGroundedPrev { get; private set; }

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

        if (isGrounded && velocity.y < GlobalVariables.instance.data.gravity * Time.deltaTime) {
            velocity.y = GlobalVariables.instance.data.gravity * Time.deltaTime;  // Need *SOME* downward input to get `controller.isGrounded` working.
        }
    }

    private void GetInput() {
        prevInput = currentInput;
        currentInput.x = Input.GetAxis("Horizontal");
        currentInput.y = Input.GetAxisRaw("Vertical");  // Binary inputs for up / down.
    }

    private void ApplyLogic() {
        isGrounded = CheckIsGrounded();

        if (isGrounded) {
            currentJumps = 0;
        }

        pusher.velocity = velocity;
        isGroundedPrev = isGrounded;
    }

    private void ApplyGravity() {
        velocity.y += GlobalVariables.instance.data.gravity * Time.deltaTime;
    }

    private void ApplyHorizontalSpeed() {
        float horizSpeed = currentInput.x * (isGrounded ? data.horizontalSpeed_Grounded : data.horizontalSpeed_Air);
        velocity.x += horizSpeed;
    }

    private void ApplyVerticalSpeed() {
        if ((currentInput.y > 0 && prevInput.y <= 0) || Input.GetButtonDown("Jump")) {
            if (isGrounded && currentJumps < data.jumpCount) {
                velocity.y = Mathf.Sqrt(data.jumpHeight * -2.0f * GlobalVariables.instance.data.gravity);
                currentJumps++;
            }
        }
    }

    private bool CheckIsGrounded() {
        RaycastHit hitInfo;
        return Physics.SphereCast(
            transform.position, controller.radius, Vector3.down, out hitInfo, 
            (controller.height / 2.0f) + (controller.skinWidth * 2.0f) - controller.radius, groundLayerMask);
    }

    public void Reset(Vector3 position) {
        currentJumps = 0;
        currentInput = Vector3.zero;
        prevInput = Vector3.zero;
        
        // CharacterController resets any `transform.position` changes. Hide our sins from it.
        controller.enabled = false;
        transform.position = position;
        controller.enabled = true;
    }

    /* INTERFACE IMPLEMENTATIONS */
    
    public void Bounce(float bounceHeight) {
        // Note that this is the same logic as jump but we reset the jump count afterwards.
        velocity.y = Mathf.Sqrt(bounceHeight * -2.0f * GlobalVariables.instance.data.gravity);
        currentJumps = 0;
    }

    public void Convey(int direction, float speed)
    {
        controller.Move(new Vector3(direction * speed, 0, 0));
    }

    /* Getters */

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public CharacterController GetController() {
        return controller;
    }
}

[CreateAssetMenu(fileName="SammyControllerData", menuName="Data/SammyControllerData")]
public class SammyControllerData : ScriptableObject {
    [Header("Ground Movement")]
    public float horizontalSpeed_Grounded;
    
    [Space, Header("Air Movement")]
    public float horizontalSpeed_Air;
    public float jumpHeight;
    public float jumpCount;
}
