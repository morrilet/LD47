using UnityEngine;

public class CharacterAnimator : MonoBehaviour {
    
    [SerializeField] private SammyController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private float rotationSpeed;

    private const float RIGHT_ROTATION = 90.0f;
    private const float LEFT_ROTATION = 260.0f;
    private float currentTarget;

    // TODO: Translate character movements into animation triggers. Alternatively, feed character controller data to
    //       the animator and it'll handle it's own transitions. That's probably best.

    void Update() {
        animator.SetFloat("VelocityX", controller.GetVelocity().x);
        animator.SetFloat("AbsVelocityX",  Mathf.Abs(controller.GetVelocity().x));
        animator.SetBool("IsGrounded", controller.isGrounded);
        if (!controller.isGrounded && controller.isGroundedPrev) {
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