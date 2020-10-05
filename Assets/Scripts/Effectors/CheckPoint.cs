using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Effector
{
    protected override void EnterAction(GameObject other)
    {
        GameManager.instance.NewCheckpoint();
    }
}
