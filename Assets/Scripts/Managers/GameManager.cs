using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        // Unlock the current scene for the player. This enables it in level_select.
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
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
    }
}
