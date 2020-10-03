using UnityEngine;

public class SpawnedPlatform : MonoBehaviour {

    private int startingLoopCount;

    private void Start() {
        startingLoopCount = GameManager.instance.currentLoopCount;
    }
}