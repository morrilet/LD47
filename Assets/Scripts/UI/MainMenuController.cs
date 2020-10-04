using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup mainMenu;
    [SerializeField] private CanvasGroup optionsMenu;
    [SerializeField] private CanvasGroup levelSelectMenu;

    [Space, Header("Volume Sliders")]
    [SerializeField] private Slider effectsVolume;
    [SerializeField] private Slider musicVolume;

    [Space, Header("Effects")]
    [SerializeField] private float transitionDuration = 0.2f;

    private void Awake() {
        // Cover for any editor issues that may arise while futzing with the menus.
        mainMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(true);

        SetMenuActive(mainMenu, true);
        SetMenuActive(optionsMenu, false);
        SetMenuActive(levelSelectMenu, false);

        effectsVolume.value = effectsVolume.maxValue;
        musicVolume.value = musicVolume.maxValue;
    }

    private void SetMenuActive(CanvasGroup canvas, bool active) {
        canvas.interactable = active;
        canvas.blocksRaycasts = active;
        canvas.alpha = active ? 1.0f : 0.0f;
    }

    private IEnumerator SetMenuActiveCoroutine(CanvasGroup canvas, bool active, float duration, float delay=0.0f) {
        float timer = 0.0f;
        float start = canvas.alpha;
        float target = active ? 1.0f : 0.0f;

        while(timer < delay) {
            yield return null;
            timer += Time.deltaTime;
        }

        timer = 0.0f;        
        while(timer <= duration) {
            canvas.alpha = Mathf.Lerp(start, target, timer / duration);

            yield return null;
            timer += Time.deltaTime;
        }

        // Set the final values when the transition is done.
        SetMenuActive(canvas, active);
    }


    /* -~-~-~-~- MENU API -~-~-~-~- */
    
    public void Play() {
        StartCoroutine(SetMenuActiveCoroutine(mainMenu, false, transitionDuration));
        StartCoroutine(SetMenuActiveCoroutine(levelSelectMenu, true, transitionDuration, transitionDuration));
    }

    public void Quit() {
        Application.Quit();
    }

    public void SwitchToOptions() {
        StartCoroutine(SetMenuActiveCoroutine(mainMenu, false, transitionDuration));
        StartCoroutine(SetMenuActiveCoroutine(optionsMenu, true, transitionDuration, transitionDuration));
    }

    public void SwitchToMainMenuFromOptions() {
        StartCoroutine(SetMenuActiveCoroutine(mainMenu, true, transitionDuration, transitionDuration));
        StartCoroutine(SetMenuActiveCoroutine(optionsMenu, false, transitionDuration));
    }

    public void SwitchToMainMenuFromLevelSelect() {
        StartCoroutine(SetMenuActiveCoroutine(mainMenu, true, transitionDuration, transitionDuration));
        StartCoroutine(SetMenuActiveCoroutine(levelSelectMenu, false, transitionDuration));
    }

    // TODO: Set the sliders to the initial value from the audio manager.

    public void SetEffectsVolume(float value) {
        AudioManager.instance.SetEffectsVolume(value);

        AudioManager.instance.PlaySound("Test");
    }

    public void SetMusicVolume(float value) {
        AudioManager.instance.SetMusicVolume(value);
    }
}
