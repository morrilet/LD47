using UnityEngine;

[System.Serializable]
public struct Emote {
    public GameObject poppableUIElement;
    public string audioEffect;
}

public class PlayerEmoteController : MonoBehaviour {

    [SerializeField] private WorldUIController uiController;
    [SerializeField] private float emoteCooldown = 1.0f;
    [SerializeField] private Emote[] emotes;

    private float timer;

    private void Update() {
        if (timer > emoteCooldown) {
            if(InputManager.instance.GetEmoteOneDown()) {
                PlayEmote(emotes[0]);
            }
            else if(InputManager.instance.GetEmoteTwoDown()) {
                PlayEmote(emotes[1]);
            }
            else if(InputManager.instance.GetEmoteThreeDown()) {
                PlayEmote(emotes[2]);
            }
            else if(InputManager.instance.GetEmoteFourDown()) {
                PlayEmote(emotes[3]);
            }
        } else {
            timer += Time.deltaTime;
        }
    }

    private void PlayEmote(Emote emote) {
        uiController.SpawnPoppableUIElement(emote.poppableUIElement);
        AudioManager.instance.PlaySound(emote.audioEffect);
        timer = 0.0f;
    }
}