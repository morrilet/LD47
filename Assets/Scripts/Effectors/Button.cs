using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Effector
{
    [SerializeField] LayerMask layerMask;

    bool pressed;

    public bool Pressed()
    {
        return pressed;
    }

    private void Start()
    {
        pressed = false;
    }

    protected override void StayAction(GameObject other)
    {
        if (!(layerMask == (layerMask | (1 << other.layer))))
        {
            pressed = true;
        }
    }

    protected override void ExitAction(GameObject other)
    {
        if (!(layerMask == (layerMask | (1 << other.layer))))
        {
            pressed = false;
        }
    }
}
