using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public float gravity = -49.05f;
    public float timeSlowScale = 0.25f;
    public float timeSlowTransitionDuration = 0.25f;

    public static GlobalVariables instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            GameObject.DestroyImmediate(this);
        }
    }
}