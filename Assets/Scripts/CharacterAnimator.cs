using UnityEngine;

public class CharacterAnimator : MonoBehaviour {
    
    [SerializeField] private SammyController controller;
    [SerializeField] private Animator animator;

    // TODO: Translate character movements into animation triggers. Alternatively, feed character controller data to
    //       the animator and it'll handle it's own transitions. That's probably best.

    void Update() {
        animator.SetFloat("VelocityX", controller.GetVelocity().x);
    }
}