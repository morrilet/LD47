using UnityEngine;

public class PlatformSpawner : MonoBehaviour {
    [SerializeField] private SammyController player;

    [Space, Header("Platform Prefabs")]
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject trampolinePrefab;
    [SerializeField] private GameObject conveyorPrefab;

    private bool canPlacePlatform = true;

    private void Update() {
        if (canPlacePlatform) {
            if (Input.GetButton("Fire3") && Input.GetButton("Jump")) {
                canPlacePlatform = false;
                PlacePlatform();
            }
        } else {
            if (Input.GetButtonUp("Jump")) {
                canPlacePlatform = true;
            }
        }

        HandleScaleTime();
    }

    public void HandleScaleTime() {

        if(Input.GetButton("Fire3") && !player.GetController().isGrounded) {
            TimeManager.instance.SetTimeScale(
                GlobalVariables.instance.data.timeSlowScale, GlobalVariables.instance.data.timeSlowTransitionDuration
            );
        }
        if (Input.GetButtonUp("Fire3") || player.GetController().isGrounded) {
            TimeManager.instance.SetTimeScale(
                1.0f, GlobalVariables.instance.data.timeSlowTransitionDuration
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
    }
}