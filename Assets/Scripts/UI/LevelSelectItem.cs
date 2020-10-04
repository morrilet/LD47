using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectItem : MonoBehaviour {
    [SerializeField] private LevelSelectItemData levelData;

    [Space]

    [SerializeField] private Image localImage;
    [SerializeField] private Button localButton;

    private void Awake() {
        if (levelData.displayImage != null) {
            localImage = levelData.displayImage;
        }
        localButton.interactable = IsSceneUnlocked();
    }

    public void LoadLevel() {
        SceneManager.LoadScene(levelData.sceneBuildIndex, LoadSceneMode.Single);
    }

    private bool IsSceneUnlocked() {
        if (!levelData.requiresUnlock)
            return true;
        return PlayerPrefs.GetInt(SceneManager.GetSceneByBuildIndex(levelData.sceneBuildIndex).name) == 1;
    }
}

[CreateAssetMenu(fileName="LevelSelectItemData", menuName="Data/LevelSelectItemData")]
public class LevelSelectItemData : ScriptableObject {
    public int sceneBuildIndex;
    public bool requiresUnlock = true;

    [Space, Header("Display")]
    public Image displayImage;
}
