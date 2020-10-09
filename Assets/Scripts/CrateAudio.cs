using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // Using our own audio source for distant 3D audio.
    [SerializeField] private AudioClip[] audioEffects;
    [SerializeField] private float minForce;  // Minimum force at which we play an effect on contact.
    [SerializeField] private float maxForce;  // Max force at which we increase the volume. Past this value volume is 1.0f.
    private float startingRotation;

    void Start()
    {
        startingRotation = transform.rotation.z;
    }

    private void OnCollisionEnter(Collision other) {
        float force = GetCollisionForce(other);
        if (force > minForce) {
            PlayRandomAudio(Mathf.Clamp01(force / maxForce));
        }
    }

    private float GetCollisionForce(Collision collision) {
        return collision.impulse.magnitude / Time.fixedDeltaTime;
    }

    private void PlayRandomAudio(float volume=1.0f) {
        audioSource.clip = audioEffects[Random.Range(0, audioEffects.Length - 1)];
        audioSource.volume = volume * AudioManager.instance.masterEffectVolume;
        audioSource.Play();
    }
}
