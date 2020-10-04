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
        Debug.Log(Mathf.Abs(player.GetVelocity().x));
        Debug.Log(player.isGroundedPrev);
        if (player.isGroundedPrev) {
            if (Mathf.Abs(player.GetVelocity().x) > 0.1f) {
                Debug.Log("CONVEYOR");
                return conveyorPrefab;
            } else {
                Debug.Log("TRAMPOLINE");
                return trampolinePrefab;
            }
        } else {
            Debug.Log("PLATFORM");
            return platformPrefab;
        }
    }

    private void PlacePlatform() {
        GameObject.Instantiate(
            GetDesiredPlatformPrefab(), transform.position, Quaternion.identity
        );
    }
}