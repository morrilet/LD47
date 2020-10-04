using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class Door : MonoBehaviour
{
    public Animator animator;
    public FloorButton doorTrigger;


    private void Update()
    {
        if (animator == null)
        {
            animator = gameObject.GetComponent<Animator>();
        }
        animator.SetBool("DoorOpen", doorTrigger.Pressed());
    }
}
