using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class Door : MonoBehaviour
{
    public Animator animator;
    public FloorButton doorTrigger;

    [SerializeField]
    private bool inverted = false;

    private void Update()
    {
        if(!inverted)
            animator.SetBool("DoorOpen", doorTrigger.Pressed());
        else
            animator.SetBool("DoorOpen", !doorTrigger.Pressed());
    }
}
