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
    private bool timeSlowed;
    private bool timeSlowedPrev;

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
                if (InputManager.instance.GetTimeshift() && InputManager.instance.GetJumpDown()) {
                    canPlacePlatform = false;
                    PlacePlatform();
                }
            } else {
                if (InputManager.instance.GetJumpUp()) {
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
        timeSlowedPrev = timeSlowed;

        if (!timeSlowed) {
            if(InputManager.instance.GetTimeshiftDown() && !player.isGrounded) {
                TimeManager.instance.SetTimeScale(
                    GlobalVariables.instance.timeSlowScale, GlobalVariables.instance.timeSlowTransitionDuration
                );
                timeSlowed = true;
            }
            if (InputManager.instance.GetTimeshift() && (!player.isGrounded && player.isGroundedPrev)) {
                TimeManager.instance.SetTimeScale(
                    GlobalVariables.instance.timeSlowScale, GlobalVariables.instance.timeSlowTransitionDuration
                );
                timeSlowed = true;
            }
        } else {
            if (InputManager.instance.GetTimeshiftUp() || (player.isGrounded && !player.isGroundedPrev)) {
                
                Debug.Log($"{player.isGrounded} -- {player.isGroundedPrev}");
                TimeManager.instance.SetTimeScale(
                    1.0f, GlobalVariables.instance.timeSlowTransitionDuration
                );
                timeSlowed = false;
            }
        }

        if (timeSlowed != timeSlowedPrev) {
            // Debug.Log($"{timeSlowed} -- {timeSlowedPrev}");
            Debug.Log("PLAY TIME SOUND");
            AudioManager.instance.PlaySound(timeSlowed ? "TimeSlow" : "TimeResume");
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