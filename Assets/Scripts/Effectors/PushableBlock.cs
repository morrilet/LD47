// using UnityEngine;
// using System.Collections.Generic;

// public class PushableBlock : Effector, IPushableTarget
// {
//     [SerializeField] LayerMask layerMask;
//     [SerializeField] LayerMask collisionLayerMask;
//     [SerializeField] private Rigidbody localRigidbody;
//     GameObject otherCollider;
//     public Collider localCollider;
//     List<Collision> collisions = new List<Collision>();

//     Vector3 velocity;

//     protected override void StayAction(GameObject other)
//     {
//         if (layerMask == (layerMask | (1 << other.layer)))
//             otherCollider = other;
//     }

//     protected override void ExitAction(GameObject other)
//     {
//         if (layerMask == (layerMask | (1 << other.layer)))
//             otherCollider = null;
//     }

//     private void FixedUpdate()
//     {
//         velocity = Vector3.zero;

//         if (otherCollider != null)
//         {
//             Push(otherCollider);
//         }

//         for (int i = 0; i < collisions.Count; i++)
//         {

//             if (collisionLayerMask == (collisionLayerMask | (1 << collisions[i].collider.gameObject.layer)))
//             {
//                 for (int j = 0; j < collisions[i].contactCount; j++)
//                 {
//                     velocity *= 1 - Mathf.Abs(Vector3.Dot(collisions[i].GetContact(j).normal, velocity.normalized));
//                 }
//             }
//         }

//         localRigidbody.AddForce(velocity, ForceMode.VelocityChange);
//     }

//     private void OnCollisionEnter(Collision collision)
//     {
//         collisions.Add(collision);
//     }

//     private void OnCollisionExit(Collision collision)
//     {
//         collisions.Remove(collision);
//     }

//     public void Push(GameObject pusher)
//     {
//         Vector3 pusherToBox = transform.position - pusher.transform.position;
//         Vector3 pusherVelocity = pusher.GetComponent<SammyController>().GetVelocity();  // TODO: Remove GetComponent call.

//         float sideCheck = Vector3.Dot(Vector3.Normalize(pusherToBox), Vector3.Normalize(pusherVelocity));

//         if(sideCheck > .7)
//         {
//             velocity = pusherVelocity;
//         }
//     }
// }
