using UnityEngine;

public class Pusher : Effector {

    [HideInInspector] public Vector3 velocity;

    protected override void StayAction(GameObject other) {
        PushableObject pushable = other.GetComponent<PushableObject>();
        if (pushable != null)
            pushable.Push(this.gameObject, velocity);
    }
}