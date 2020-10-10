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

        AudioManager.instance.PlaySound("MainMusic");
    }

    private void Update()
    {
        if(platformQueue.Count > maxPlatforms)
        {
            SpawnedPlatform oldPlatform = platformQueue.Peek();
            oldPlatform.Deactivate();
            platformQueue.Dequeue();
        }

        if(InputManager.instance.GetReset())
        {
            if(resetTimer < resetTimerMax) {
                resetTimer += Time.deltaTime;
            } else {
                if (InputManager.instance.GetTimeshift()) {
                    ResetPlatforms();
                } else {
                    KillPlayer();
                }
                resetTimer = 0;
            }
            
            // Reset the timer if we switch from platform clear to player clear or vice versa.
            if (InputManager.instance.GetTimeshiftUp() || InputManager.instance.GetTimeshiftDown()) {
                resetTimer = 0;
            }
        }

        if (InputManager.instance.GetResetUp()) {
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

    public void ResetPlatforms() {
        // Perf nightmare but I'm past caring. Gotta go fast.
        SpawnedPlatform[] allPlatforms = Object.FindObjectsOfType<SpawnedPlatform>();
        Debug.Log(allPlatforms);
        for (int i = 0; i < allPlatforms.Length; i++) {
            GameObject.Destroy(allPlatforms[i].gameObject);
        }
        platformQueue = new Queue<SpawnedPlatform>();
    }

    public void NextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
