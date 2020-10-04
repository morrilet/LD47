using UnityEngine;

public class SpawnedPlatform : MonoBehaviour {

    [SerializeField] private int activeLoopDelay;  // The number of loops before we're use-able.
    [SerializeField] private int activeLoopDuration;  // The number of loops we remain use-able.

    [Space, Header("Activation")]
    [SerializeField] private Collider[] colliders;
    [SerializeField] private GameObject[] persistObjects;
    [SerializeField] private ParticleSystem particleEffect;
    [SerializeField] private float destroyPersistentObjectDelay;

    private int startingLoopCount;
    private bool isActive;

    private void Start() {
        startingLoopCount = GameManager.instance.currentLoopCount;
        isActive = false;
        
        for(int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = false;
        }
    }

    private void Update() {
        int currentLoop = GameManager.instance.currentLoopCount;

        if (!isActive && currentLoop == startingLoopCount + activeLoopDelay)
            Activate();
        
        if (isActive && currentLoop == startingLoopCount + activeLoopDelay + activeLoopDuration + 1)
            Deactivate();
    }

    private void Activate() {
        isActive = true;
        
        for(int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = true;
        }

        particleEffect.Play();
    }

    private void Deactivate() {
        isActive = false;

        for(int i = 0; i < persistObjects.Length; i++) {
            persistObjects[i].transform.parent = null;
            GameObject.Destroy(persistObjects[i], destroyPersistentObjectDelay);
        }

        GameObject.Destroy(this.gameObject);
    }
}