using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Door : MonoBehaviour
{
    public Animator animator;
    public FloorButton doorTrigger;

    [SerializeField] private bool inverted = false;
    private bool currentState;

    private void Start() {
        animator.SetBool("DoorOpen", inverted);  // Not using SetDoorState b/c we don't want audio this time.
    }

    private void Update()
    {
        UpdateDoorState();
    }

    private void UpdateDoorState() {

        if (currentState != doorTrigger.Pressed()) {
            currentState = doorTrigger.Pressed();

            if(!inverted)
                SetDoorState(currentState);
            else
                SetDoorState(!currentState);
        }
    }

    private void SetDoorState(bool state) {
        AudioManager.instance.PlaySound("Door");
        animator.SetBool("DoorOpen", state);
    }
}
