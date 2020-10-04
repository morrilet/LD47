using UnityEngine;

public class MurderZone : Effector {

    [SerializeField] private LayerMask playerLayerMask;
    private bool triggered = false;

    protected override void EnterAction(GameObject other) {
        if (playerLayerMask == (playerLayerMask | (1 << other.layer)) && !triggered) {
            GameManager.instance.KillPlayer();
            triggered = true;
        }
    }

    protected override void ExitAction(GameObject other) {
        if (playerLayerMask == (playerLayerMask | (1 << other.layer))) {
            triggered = false;
        }
    }
}