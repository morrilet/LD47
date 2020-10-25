using UnityEngine;

public class MurderZone : Effector {

    [SerializeField] private LayerMask playerLayerMask;
    private bool triggered = false;

    private void Start() {
        GameManager.instance.onPlayerReset += HandlePlayerReset;
    }

    private void OnDestroy() {
        GameManager.instance.onPlayerReset -= HandlePlayerReset;
    }

    protected override void EnterAction(GameObject other) {
        if (playerLayerMask == (playerLayerMask | (1 << other.layer)) && !triggered) {
            GameManager.instance.KillPlayer(GameManager.CauseOfDeath.Acid);
            triggered = true;
        }
    }

    protected override void ExitAction(GameObject other) {
        if (playerLayerMask == (playerLayerMask | (1 << other.layer))) {
            triggered = false;
        }
    }

    // When the player is reset they don't trigger ExitAction, so we also need to check for that.
    // In fact, we'll likely never trigger ExitAction but it's best to keep it just in case.
    public void HandlePlayerReset() {
        triggered = false;
    }
}