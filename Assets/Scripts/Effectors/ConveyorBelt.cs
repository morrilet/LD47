using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : Effector
{
    //Direction should be a value of -1 or 1
    public int direction;

    public void Start()
    {
        Vector3 directionHolder = transform.localScale;

        directionHolder.x = directionHolder.x * direction;

        transform.localScale = directionHolder;
    }

    [SerializeField] private float speed;

    protected override void StayAction(GameObject other)
    {
        IConveyorBeltTarget target = other.GetComponent<IConveyorBeltTarget>();
        if(target != null)
        {
            target.Convey(direction, speed);
        }
    }
}
