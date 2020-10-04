using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicTransform : MonoBehaviour
{
    [SerializeField] private Transform target;

    void LateUpdate() {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
