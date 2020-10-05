using UnityEngine;

public class Pedestal : Effector {

    [SerializeField] private WorldUIController uIController;
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private SammyHeadLights sammyHeadLights;

    bool canInteract;
    bool hasInteracted;

    private void Start() {
        platformSpawner.enabled = false;
        sammyHeadLights.SetHeadlightsEnabled(false);
    }

    private void Update() {
        if(canInteract && !hasInteracted) {
            if (Input.GetButtonDown("Fire3")) {
                Interact();
            }
        }
    }

    private void Interact() {
        hasInteracted = true;
        platformSpawner.enabled = true;
        sammyHeadLights.SetHeadlightsEnabled(true);
        uIController.HidePersistentUIElement("Interact");
    }

    protected override void EnterAction(GameObject other) {
        if (!hasInteracted) {
            canInteract = true;
            uIController.ShowPersistentUIElement("Interact");
        }
    }

    protected override void ExitAction(GameObject other) {
        if (!hasInteracted) {
            canInteract = false;
            uIController.HidePersistentUIElement("Interact");
        }
    }
}