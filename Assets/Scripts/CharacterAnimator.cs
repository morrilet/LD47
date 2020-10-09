using UnityEngine;

public class CharacterAnimator : MonoBehaviour {
    
    [SerializeField] private SammyController controller;
    [SerializeField] private Animator animator;
    // [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpVelocityThreshold;

    private const float RIGHT_ROTATION = 90.0f;
    private const float LEFT_ROTATION = 260.0f;
    private float currentTarget = RIGHT_ROTATION;
    private Vector3 tempVector;
    
    void Update() {
        tempVector = controller.GetVelocity();

        animator.SetFloat("VelocityX", tempVector.x);
        animator.SetFloat("AbsVelocityX",  Mathf.Max(0.05f, Mathf.Abs(tempVector.x)));
        animator.SetBool("IsGrounded", controller.isGrounded);
        if (!controller.isGrounded && controller.isGroundedPrev && tempVector.y >= jumpVelocityThreshold) {
            animator.SetTrigger("Jump");
        }

        RotateTowardVelocity();
    }

    void RotateTowardVelocity() {
        if (controller.GetVelocity().x > 0) {
            currentTarget = RIGHT_ROTATION;
        } else if (controller.GetVelocity().x < 0) {
            currentTarget = LEFT_ROTATION;
        }

        transform.rotation = Quaternion.Euler(0, currentTarget, 0);
    }
}