using UnityEngine;
using System.Collections.Generic;

public class PushableBlock : Effector, IPushableBlockTarget
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask collisionLayerMask;

    public Rigidbody rigid;
    GameObject otherCollider;
    public Collider collider;
    List<Collision> collisions = new List<Collision>();

    Vector3 velocity;

    protected override void StayAction(GameObject other)
    {
        if (layerMask == (layerMask | (1 << other.layer)))
        {
            otherCollider = other;
        }
    }

    protected override void ExitAction(GameObject other)
    {
        otherCollider = null;
    }

    private void FixedUpdate()
    {
        velocity = Vector3.zero;

        if (otherCollider != null)
        {
            Push(otherCollider);
        }

        for (int i = 0; i < collisions.Count; i++)
        {

            if (collisionLayerMask == (collisionLayerMask | (1 << collisions[i].collider.gameObject.layer)))
            {
                Debug.Log("If check");
                

                for (int j = 0; j < collisions[i].contactCount; j++)
                {
                    Debug.DrawRay(collisions[i].GetContact(j).point, collisions[i].GetContact(j).normal * 2);
 //                   velocity *= 1 - Mathf.Abs(Vector3.Dot(collisions[i].GetContact(j).normal, velocity));
                }
            }
        }

        Debug.Log(velocity);

        rigid.AddForce(velocity, ForceMode.VelocityChange);

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions.Add(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        collisions.Remove(collision);
    }

    public void Push(GameObject pusher)
    {
        Vector3 pusherToBox = transform.position - pusher.transform.position;
        Vector3 pusherVelocity = pusher.GetComponent<SammyController>().GetVelocity();

        float sideCheck = Vector3.Dot(Vector3.Normalize(pusherToBox), Vector3.Normalize(pusherVelocity));

        if(sideCheck > .7)
        {
            velocity = -pusherVelocity;
        }
    }
}
