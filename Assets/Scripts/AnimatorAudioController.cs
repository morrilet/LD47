using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorAudioController : MonoBehaviour
{
    string[] footsteps = new string[] {
        "Footstep_0",
        "Footstep_1",
        "Footstep_2",
        "Footstep_3"
    };
    string tempEffect;

    void PlayEffect(string effectName) {
        AudioManager.instance.PlaySound(effectName);
    }

    void PlayRandomFootstep() {
        tempEffect = footsteps[Random.Range(0, footsteps.Length - 1)];
        PlayEffect(tempEffect);
    }
}
