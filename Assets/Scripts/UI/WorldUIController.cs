using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct PoppableUIElementPrefab
{
    public string name;
    public GameObject prefab;
}

public class WorldUIController : MonoBehaviour
{
    [SerializeField] private PoppableUIElementPrefab[] poppableUIElements;

    public void SpawnPoppableUIElement(string name) {
        PoppableUIElementPrefab element = GetPoppableUIElement(name);
        GameObject.Instantiate(element.prefab, transform.position, Quaternion.identity, transform);
    }

    private PoppableUIElementPrefab GetPoppableUIElement(string name) {
        for (int i = 0; i < poppableUIElements.Length; i++) {
            if (poppableUIElements[i].name == name)
                return poppableUIElements[i];
        }

        Debug.LogWarning($"PoppableUIElement with name '{name}' not found on GameObject called '{gameObject.name}'");
        return new PoppableUIElementPrefab();
    }

    // For debugging...
    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            SpawnPoppableUIElement("Test");
        }
    }
}
