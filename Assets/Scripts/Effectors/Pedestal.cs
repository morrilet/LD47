using UnityEngine;

public class Pedestal : Effector {

    [SerializeField] private WorldUIController uIController;
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private SammyHeadLights sammyHeadLights;
    [SerializeField] private bool isReturn;
    [SerializeField] private bool blockInteract;
    [SerializeField] private GameObject levelEnder;

    bool canInteract;
    bool hasInteracted;

    private void Start() {
        if (!blockInteract) {
            platformSpawner.enabled = isReturn;
            sammyHeadLights.SetHeadlightsEnabled(isReturn);
        }
    }

    private void Update() {
        if (blockInteract) 
            canInteract = false;

        if(canInteract && !hasInteracted) {
            if (Input.GetButtonDown("Fire3")) {
                Interact();
            }
        }
    }

    private void Interact() {
        hasInteracted = true;
        platformSpawner.enabled = !isReturn;
        sammyHeadLights.SetHeadlightsEnabled(!isReturn);
        uIController.HidePersistentUIElement("Interact");

        if (isReturn) {
            Invoke("EndLevel", 3.0f);
        }
    }

    private void EndLevel() {
        levelEnder.SetActive(true);
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