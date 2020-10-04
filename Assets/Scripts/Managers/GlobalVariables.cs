using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables instance;
    public GlobalVariablesData data;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            GameObject.DestroyImmediate(this);
        }
    }
}

[CreateAssetMenu(fileName="GlobalVariablesData", menuName="Data/GlobalVariablesData")]
public class GlobalVariablesData : ScriptableObject {

    public float gravity;
    public float timeSlowScale;
    public float timeSlowTransitionDuration;
}