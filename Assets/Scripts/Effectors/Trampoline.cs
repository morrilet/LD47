using UnityEngine;

public class Trampoline : Effector {

    [SerializeField] float bounceHeight;
    
    protected override void EnterAction(GameObject other) {
        ITrampolineTarget target = other.GetComponent<ITrampolineTarget>();
        if(target != null) {
            target.Bounce(bounceHeight);
        }
    }
}