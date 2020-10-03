using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int currentLoopCount { get; private set; }

    [SerializeField] private SammyController player;
    private Vector3 respawnPosition;

    private void Awake() {
        if (instance != null)
            GameObject.DestroyImmediate(this.gameObject);
        instance = this;
    }

    private void Start() {
        respawnPosition = player.transform.position;
    }

    public void UpdateRespawnPosition(Vector3 position) {
        respawnPosition = position;
    }

    public void KillPlayer() {
        currentLoopCount += 1;
        player.Reset(respawnPosition);

        Debug.Log($"NEW LOOP COUNT: {currentLoopCount}");
    } 
}
