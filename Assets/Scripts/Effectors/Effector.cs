using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effector : MonoBehaviour
{
    [SerializeField] private EffectorTrigger[] triggers;

    private void Awake()
    {
        for (int i = 0; i < triggers.Length; ++i)
        {
            triggers[i].triggerEnter += EnterAction;
            triggers[i].triggerStay += StayAction;
            triggers[i].triggerExit += ExitAction;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < triggers.Length; ++i)
        {
            triggers[i].triggerEnter -= EnterAction;
            triggers[i].triggerStay -= StayAction;
            triggers[i].triggerExit -= ExitAction;
        }
    }

    protected virtual void EnterAction(GameObject other) { }
    protected virtual void StayAction(GameObject other) { }
    protected virtual void ExitAction(GameObject other) { }
}
