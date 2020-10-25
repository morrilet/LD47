using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Queue<SpawnedPlatform> platformQueue;
    private int maxPlatforms = 9;

    public float resetTimer { get; private set; }
    public float resetTimerMax { get; private set; }

    public int currentLoopCount { get; private set; }

    [SerializeField] private SammyController player;

    private Vector3 respawnPosition;
    private bool hasFreezePlayerRequest;
    private AudioSource resetSource;

    public delegate void PlayerResetAction();
    public PlayerResetAction onPlayerReset;

    public enum CauseOfDeath {
        Reset,
        Acid,
    }

    private void Awake() {
        if (instance != null)
            GameObject.DestroyImmediate(this.gameObject);
        instance = this;

        // Unlock the current scene for the player. This enables it in level_select.
        Debug.Log($"Setting player prefs for {SceneManager.GetActiveScene().name}");
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex + "", 1);
    }

    private void Start() {
        respawnPosition = player.transform.position;
        platformQueue = new Queue<SpawnedPlatform>();
        resetTimerMax = 1.25f;

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

        /* -~-~-~-~- RESET HANDLING -~-~-~-~-*/
        if (InputManager.instance.GetResetDown()) {
            resetSource = AudioManager.instance.PlaySound("Reset");
        }

        if(InputManager.instance.GetReset())
        {
            if(resetTimer < resetTimerMax) {
                resetTimer += Time.deltaTime;
            } else {
                if (InputManager.instance.GetTimeshift()) {
                    ResetPlatforms();
                } else {
                    KillPlayer(GameManager.CauseOfDeath.Reset);
                }
                resetTimer = 0;
            }
            
            // Reset the timer if we switch from platform clear to player clear or vice versa.
            if (InputManager.instance.GetTimeshiftUp() || InputManager.instance.GetTimeshiftDown()) {
                resetTimer = 0;
            }
        }

        if (InputManager.instance.GetResetUp()) {
            resetSource.Stop();
            resetTimer = 0;
        }
        /* -~-~-~-~- END RESET HANDLING -~-~-~-~-*/

        if (resetTimer > 0 && player.isGrounded) {
            FreezePlayer();
        }
        UpdateFreezePlayer();
    }

    private void UpdateFreezePlayer() {
        // The idea here is that if ANY script requests to freeze the player we do it, but only for that
        // frame. As scripts no longer need to freeze the player they'll stop requesting it and we can
        // safely resume the player. This keeps various scripts from stepping on each others toes.
        player.canMove = !hasFreezePlayerRequest;
        hasFreezePlayerRequest = false;
    }

    private void ResetPlayer() {
        player.gameObject.SetActive(true);
        player.Reset(respawnPosition);

        if (onPlayerReset != null) {
            onPlayerReset();
        }
    }

    public void FreezePlayer() {
        hasFreezePlayerRequest = true;
    }

    public void UpdateRespawnPosition(Vector3 position) {
        respawnPosition = position;
    }

    public void KillPlayer(CauseOfDeath reason) {
        currentLoopCount += 1;

        if (reason == CauseOfDeath.Acid) {
            player.gameObject.SetActive(false);
            AudioManager.instance.PlaySound("Acid");
            Invoke("ResetPlayer", 2.5f);  // 2.5s is the duration of the acid audio effect.
        } 
        else if (reason == CauseOfDeath.Reset) {
            ResetPlayer();
        }
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
