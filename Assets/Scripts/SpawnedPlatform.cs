using UnityEngine;

public class SpawnedPlatform : MonoBehaviour {

    [SerializeField] private Renderer localRenderer;
    [SerializeField] private int activeLoopDelay;  // The number of loops before we're use-able.
    [SerializeField] private int activeLoopDuration;  // The number of loops we remain use-able.

    [Space, Header("Activation")]
    [SerializeField] private Collider[] colliders;
    [SerializeField] private GameObject[] persistObjects;
    [SerializeField] private ParticleSystem particleEffect;
    [SerializeField] private float destroyPersistentObjectDelay;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private GameObject[] activeObjects;
    private string activeLayer = "Default";
    private string inactiveLayer = "TemporaryPlatform";

    private int startingLoopCount;
    private bool isActive;
    private int startingLayer;

    private void Start() {
        startingLoopCount = GameManager.instance.currentLoopCount;
        isActive = false;
        
        for(int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = false;
        }
        
        if (localRenderer != null)
            localRenderer.material = inactiveMaterial;

        for (int i = 0; i < activeObjects.Length; i++) {
            activeObjects[i].SetActive(false);
        }

        this.gameObject.layer = LayerMask.NameToLayer(inactiveLayer);
    }

    private void Update() {
        int currentLoop = GameManager.instance.currentLoopCount;

        if (!isActive && currentLoop == startingLoopCount + activeLoopDelay)
            Activate();
    }

    private void Activate() {
        isActive = true;
        GameManager.instance.EnquePlatform(this);
        this.gameObject.layer = LayerMask.NameToLayer(activeLayer);

        for(int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = true;
        }

        if (localRenderer != null)
            localRenderer.material = activeMaterial;

        for (int i = 0; i < activeObjects.Length; i++) {
            activeObjects[i].SetActive(true);
        }
        particleEffect.Play();
    }

    public void Deactivate() {
        isActive = false;
        this.gameObject.layer = LayerMask.NameToLayer(inactiveLayer);

        for(int i = 0; i < persistObjects.Length; i++) {
            persistObjects[i].transform.parent = null;
            GameObject.Destroy(persistObjects[i], destroyPersistentObjectDelay);
        }

        GameObject.Destroy(this.gameObject);
    }
}