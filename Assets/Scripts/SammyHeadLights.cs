using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SammyHeadLights : MonoBehaviour
{
    [SerializeField] private Material[] lightMaterials;
    [SerializeField] private PlatformSpawner localPlatformSpawner;
    [SerializeField] private Renderer localMeshRenderer;

    private Material[] materialHolder;

    private void Start()
    {
        materialHolder = localMeshRenderer.materials;
    }

    private void Update()
    {
        materialHolder[1] = lightMaterials[3 - localPlatformSpawner.PlatformsThisLoop()];
        localMeshRenderer.materials = materialHolder;
    }
}
