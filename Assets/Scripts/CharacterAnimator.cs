using UnityEngine;

public class CharacterAnimator : MonoBehaviour {
    
    [SerializeField] private SammyController controller;
    [SerializeField] private PlayerEmoteController emoteController;
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
        animator.SetBool("Reset", GameManager.instance.resetTimer != 0 && controller.isGrounded);
        animator.SetFloat("ResetSpeed", 1.0f / GameManager.instance.resetTimerMax);
        if ((emoteController.isEmoting && !emoteController.isEmotingPrev) && controller.isGrounded) {
            animator.SetTrigger("Emote");
        }
        animator.SetFloat("EmoteSpeed", 1.0f / emoteController.GetEmoteCooldown());

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