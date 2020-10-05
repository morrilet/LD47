using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Queue<SpawnedPlatform> platformQueue;
    private int maxPlatforms = 9;

    private float resetTimer;
    private float resetTimerMax;

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
        platformQueue = new Queue<SpawnedPlatform>();
        resetTimerMax = 1f;
    }

    private void Update()
    {
        if(platformQueue.Count > maxPlatforms)
        {
            Debug.Log("Max Platforms reached");

            SpawnedPlatform oldPlatform = platformQueue.Peek();
            oldPlatform.Deactivate();
            platformQueue.Dequeue();
        }

        if(Input.GetButton("Restart"))
        {
            if(resetTimer < resetTimerMax)
            {
                resetTimer += Time.deltaTime;
            }
            else
            {
                KillPlayer();
                resetTimer = 0;
            }
        }

        if (Input.GetButtonUp("Restart"))
        {
            resetTimer = 0;
        }
    }

    public void UpdateRespawnPosition(Vector3 position) {
        respawnPosition = position;
    }

    public void KillPlayer() {
        currentLoopCount += 1;
        player.Reset(respawnPosition);
    }

    public void NewCheckpoint()
    {
        respawnPosition = player.transform.position;
    }

    public void EnquePlatform(SpawnedPlatform platform)
    {
        platformQueue.Enqueue(platform);
    }
}
