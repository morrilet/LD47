using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct LevelSelectItemData {
    public int sceneBuildIndex;
    public bool noUnlockRequired;

    [Space, Header("Display")]
    public Image displayImage;
}

public class LevelSelectItem : MonoBehaviour {
    [SerializeField] private LevelSelectItemData levelData;

    [Space]

    [SerializeField] private Image localImage;
    [SerializeField] private Button localButton;

    private void Awake() {
        if (levelData.displayImage != null) {
            localImage = levelData.displayImage;
        }
    }

    private void Start() {
        localButton.interactable = IsSceneUnlocked();
    }

    public void LoadLevel() {
        SceneManager.LoadScene(levelData.sceneBuildIndex, LoadSceneMode.Single);
    }

    private bool IsSceneUnlocked() {
        if (levelData.noUnlockRequired)
            return true;
        return PlayerPrefs.GetInt(levelData.sceneBuildIndex.ToString()) == 1;
    }
}
