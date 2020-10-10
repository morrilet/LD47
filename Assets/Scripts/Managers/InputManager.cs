using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Inputs {
    public bool jump, jumpPrev;
    public bool timeshift, timeshiftPrev;
    public bool reset, resetPrev;
    public bool emoteOne, emoteOnePrev;
    public bool emoteTwo, emoteTwoPrev;
    public bool emoteThree, emoteThreePrev;
    public bool emoteFour, emoteFourPrev;

    public void ResetInputs() {
        jumpPrev = jump;
        timeshiftPrev = timeshift;
        resetPrev = reset;
        emoteOnePrev = emoteOne;
        emoteTwoPrev = emoteTwo;
        emoteThreePrev = emoteThree;
        emoteFourPrev = emoteFour;

        jump = false;
        timeshift = false;
        reset = false;
        emoteOne = false;
        emoteTwo = false;
        emoteThree = false;
        emoteFour = false;
    }
}

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    
    private Inputs inputs;

    private void Awake()
    {
        if (instance != null)
            GameObject.DestroyImmediate(this.gameObject);
        instance = this;
    }

    private void Update() {
        // Collect input at the top of the frame (note the early script execution order.)
        getJumpInput();
        getTimeshiftInput();
        getResetInput();
        getEmoteOneInput();
        getEmoteTwoInput();
        getEmoteThreeInput();
        getEmoteFourInput();
    }

    private void LateUpdate() {
        // Reset inputs at the end of the frame.
        inputs.ResetInputs();
    }

    /* INPUT POLLING */

    void getJumpInput() {
        inputs.jump = Input.GetButton("Jump");
    }

    void getTimeshiftInput() {
        inputs.timeshift = Input.GetButton("Fire3") || Input.GetAxisRaw("Fire3") == 1;
    }

    void getResetInput() {
        inputs.reset = Input.GetButton("Restart");
    }

    void getEmoteOneInput() {
        inputs.emoteOne = Input.GetButton("Emote1") || Input.GetAxisRaw("Emote1") == -1;
    }

    void getEmoteTwoInput() {
        inputs.emoteTwo = Input.GetButton("Emote2") || Input.GetAxisRaw("Emote2") == 1;
    }

    void getEmoteThreeInput() {
        inputs.emoteThree = Input.GetButton("Emote3") || Input.GetAxisRaw("Emote3") == 1;
    }

    void getEmoteFourInput() {
        inputs.emoteFour = Input.GetButton("Emote4") || Input.GetAxisRaw("Emote4") == -1;
    }

    /* PUBLIC API */

    public bool GetJump() { return inputs.jump; }
    public bool GetJumpDown() { return inputs.jump && ! inputs.jumpPrev; }
    public bool GetJumpUp() { return !inputs.jump && inputs.jumpPrev; }

    public bool GetTimeshift() { return inputs.timeshift; }
    public bool GetTimeshiftDown() { return inputs.timeshift && ! inputs.timeshiftPrev; }
    public bool GetTimeshiftUp() { return !inputs.timeshift && inputs.timeshiftPrev; }

    public bool GetReset() { return inputs.reset; }
    public bool GetResetDown() { return inputs.reset && ! inputs.resetPrev; }
    public bool GetResetUp() { return !inputs.reset && inputs.resetPrev; }

    public bool GetEmoteOneDown() { return inputs.emoteOne && !inputs.emoteOnePrev; }
    public bool GetEmoteTwoDown() { return inputs.emoteTwo && !inputs.emoteTwoPrev; }
    public bool GetEmoteThreeDown() { return inputs.emoteThree && !inputs.emoteThreePrev; }
    public bool GetEmoteFourDown() { return inputs.emoteFour && !inputs.emoteFourPrev; }
}
