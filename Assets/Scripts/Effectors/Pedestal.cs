using UnityEngine;

public class Pedestal : Effector {

    [SerializeField] private WorldUIController uIController;

    bool canInteract;

    protected override void EnterAction(GameObject other) {
        canInteract = true;
        uIController.ShowPersistentUIElement("Interact");
    }

    protected override void ExitAction(GameObject other) {
        canInteract = false;
        uIController.HidePersistentUIElement("Interact");
    }
}