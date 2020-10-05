using UnityEngine;
using System.Collections.Generic;

public class PushableObject : MonoBehaviour, ITrampolineTarget, IConveyorBeltTarget {

    [SerializeField] private LayerMask geometryLayerMask;
    [SerializeField] private Rigidbody localRigidbody;
    private List<Collision> collisions = new List<Collision>();

    public Pusher localPusher;

    private Vector3 velocity;
    private bool shouldPush = false;
    private GameObject pusher;
    private Vector3 pusherVelocity;

    private Vector3 startPosition;
    private Vector3 startRotation;
    private int lastLoop;

    private float heightFromBounce;
    private bool isBouncing = false;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation.eulerAngles;
        lastLoop = GameManager.instance.currentLoopCount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions.Add(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        collisions.Remove(collision);
    }

    private void Update()
    {
        if (lastLoop != GameManager.instance.currentLoopCount)
        {
            transform.position = startPosition;
            transform.rotation = Quaternion.Euler(startRotation);
            localRigidbody.velocity = new Vector3(0, 0, 0);

            lastLoop = GameManager.instance.currentLoopCount;
        }
    }

    private void FixedUpdate()
    {
        velocity = Vector3.zero;

        if(shouldPush) {
            HandlePush(pusher, pusherVelocity);
        }

        // Get the negative force from level geometry.
        for (int i = 0; i < collisions.Count; i++) {
            if (geometryLayerMask == (geometryLayerMask | (1 << collisions[i].collider.gameObject.layer)))
                for (int j = 0; j < collisions[i].contactCount; j++)
                    velocity *= 1 - Mathf.Abs(Vector3.Dot(collisions[i].GetContact(j).normal, velocity.normalized));
        }

        localPusher.velocity = velocity;

        // Apply velocity to the RB.
        localRigidbody.AddForce(velocity, ForceMode.Force);
    }

    public void Push(GameObject pusher, Vector3 velocity) {
        // This method simply tells us that we need to push. We handle push ourselves 
        // so it doesn't get called multiple times per frame.
        shouldPush = true;
        this.pusher = pusher;
        pusherVelocity = velocity;
    }

    protected virtual void HandlePush(GameObject pusher, Vector3 velocity) { 

        Vector3 pusherToSelf = transform.position - pusher.transform.position;
        float sideCheck = Vector3.Dot(Vector3.Normalize(pusherToSelf), Vector3.Normalize(velocity));

        // 0.7 is a bit of a magic number. It ensures that we can't be pushed from directly above.
        if(sideCheck > 0.7f) {
            this.velocity = velocity;
        }

        shouldPush = false;
    }

    public void Bounce(float bounceHeight)
    {
        //TODO: Amplify Horizontal velocity
        localRigidbody.AddForce(new Vector3(0, bounceHeight * 1.2f, 0), ForceMode.VelocityChange);
    }

    public void Convey(int direction, float speed)
    {
        localRigidbody.AddForce(new Vector3(direction * speed * 12, 0, 0), ForceMode.VelocityChange);
        Debug.Log(localRigidbody.velocity);
    }
}
