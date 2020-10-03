using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorTrigger : MonoBehaviour {

    public delegate void TriggerAction(GameObject other);
    public event TriggerAction triggerEnter;
    public event TriggerAction triggerStay;
    public event TriggerAction triggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if(triggerEnter != null)
            triggerEnter(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(triggerStay != null)
            triggerStay(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if(triggerExit != null)
            triggerExit(other.gameObject);
    }
}