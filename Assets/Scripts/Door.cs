using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class Door : MonoBehaviour
{
    public Animator animator;
    public Button doorTrigger;


    private void Update()
    {
        animator.SetBool("DoorOpen", doorTrigger.Pressed());
    }
}
