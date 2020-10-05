using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateRotationAudio : MonoBehaviour
{
    public float startingRotation;

    void Start()
    {
        startingRotation = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
