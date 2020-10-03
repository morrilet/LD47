using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectItem : MonoBehaviour {
    [SerializeField] private LevelSelectItemData levelData;

    [Space]

    [SerializeField] private Image localImage;

    private void Awake() {
        if (levelData.displayImage != null) {
            localImage = levelData.displayImage;
        }
    }

    public void LoadLevel() {
        SceneManager.LoadScene(levelData.sceneBuildIndex, LoadSceneMode.Single);
    }
}

[CreateAssetMenu(fileName="LevelSelectItemData", menuName="Data/LevelSelectItemData")]
public class LevelSelectItemData : ScriptableObject {
    public int sceneBuildIndex;

    [Space, Header("Display")]
    public Image displayImage;
}
