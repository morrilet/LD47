using UnityEngine;

public class Trampoline : Effector {

    [SerializeField] float bounceHeight;
    [SerializeField] LayerMask layerMask;

    protected override void EnterAction(GameObject other) {

        if (layerMask == (layerMask | (1 << other.layer))) {
            other.GetComponent<ITrampolineTarget>().Bounce(bounceHeight);
        }
    }
}