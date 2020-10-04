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
            if(Input.GetButtonDown("Emote1")) {
                PlayEmote(emotes[0]);
            }
            else if(Input.GetButtonDown("Emote2")) {
                PlayEmote(emotes[1]);
            }
            else if(Input.GetButtonDown("Emote3")) {
                PlayEmote(emotes[2]);
            }
            else if(Input.GetButtonDown("Emote4")) {
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