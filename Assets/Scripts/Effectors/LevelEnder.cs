using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnder : Effector
{
    [SerializeField] private LayerMask layerMask;

    protected override void EnterAction(GameObject other)
    {
        if(layerMask == (layerMask | (1 << other.layer)))
        {
            GameManager.instance.NextLevel();
        }
    }
}
