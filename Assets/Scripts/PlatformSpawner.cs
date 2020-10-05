using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour {
    [SerializeField] private SammyController player;

    [Space, Header("Platform Prefabs")]
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject trampolinePrefab;
    [SerializeField] private GameObject conveyorPrefab;

    private int platformsThisLoop;
    private int maxPlatformsPerLoop;
    private int startingLoopCount;

    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    public int PlatformsThisLoop() { return platformsThisLoop; }

    [SerializeField] private float platformDoubleJumpHeight;

    private bool canPlacePlatform = true;

    private void Start()
    {
        platformsThisLoop = 0;
        maxPlatformsPerLoop = 3;
        startingLoopCount = GameManager.instance.currentLoopCount;
    }

    private void Update() {
        if (!player.isGroundedOnLevelObj && !player.isGroundedOnLevelObjPrev)
        {
            if (canPlacePlatform && platformsThisLoop < maxPlatformsPerLoop) {
                if (Input.GetButton("Fire3") && Input.GetButtonDown("Jump")) {
                    canPlacePlatform = false;
                    PlacePlatform();
                }
            } else {
                if (Input.GetButtonUp("Jump")) {
                    canPlacePlatform = true;
                }
            }
        }

        if (startingLoopCount != GameManager.instance.currentLoopCount)
            platformsThisLoop = 0;

        startingLoopCount = GameManager.instance.currentLoopCount;

        HandleScaleTime();
    }

    public void HandleScaleTime() {

        if(Input.GetButtonDown("Fire3") && !player.isGrounded) {
            TimeManager.instance.SetTimeScale(
                GlobalVariables.instance.timeSlowScale, GlobalVariables.instance.timeSlowTransitionDuration
            );
        }
        if (Input.GetButtonUp("Fire3") || (player.isGrounded && !player.isGroundedPrev)) {
            TimeManager.instance.SetTimeScale(
                1.0f, GlobalVariables.instance.timeSlowTransitionDuration
            );
        }
        if (Input.GetButton("Fire3") && (!player.isGrounded && player.isGroundedPrev)) {
            TimeManager.instance.SetTimeScale(
                GlobalVariables.instance.timeSlowScale, GlobalVariables.instance.timeSlowTransitionDuration
                );
        }
    }

    private GameObject GetDesiredPlatformPrefab() {
        if (player.isGroundedPrev) {
            if (Mathf.Abs(player.GetVelocity().x) > 0.1f) {
                return conveyorPrefab;
            } else {
                return trampolinePrefab;
            }
        } else {
            return platformPrefab;
        }
    }

    private void PlacePlatform() {
        GameObject newObj = GameObject.Instantiate(GetDesiredPlatformPrefab(), transform.position, Quaternion.identity);
        
        ConveyorBelt belt = newObj.GetComponent<ConveyorBelt>();
        if (belt != null) {
            belt.direction = (int)Mathf.Sign(player.GetVelocity().x);
        }

        if (!player.isGrounded)
            player.Bounce(platformDoubleJumpHeight);

        platformsThisLoop++;
    }
}