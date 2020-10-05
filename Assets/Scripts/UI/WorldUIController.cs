using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct UIElementPrefab {
    public string name;
    public GameObject prefab;
}

public struct CachedUIElement {
    public string name;
    public GameObject cachedObject;
}

public class WorldUIController : MonoBehaviour
{
    [SerializeField] private UIElementPrefab[] uiElements;
    private List<CachedUIElement> persistentUIElementCache = new List<CachedUIElement>();

    public void SpawnPoppableUIElement(string name) {
        SpawnPoppableUIElement(GetUIElement(name));
    }

    public void SpawnPoppableUIElement(UIElementPrefab uiElement) {
        SpawnPoppableUIElement(uiElement.prefab);
    }

    public void SpawnPoppableUIElement(GameObject prefab) {
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }

    public void ShowPersistentUIElement(string name) {
        CachedUIElement element = GetPersistentUIElement(name);
        element.cachedObject.GetComponent<PersistentUIElement>().Open();
    }

    public void HidePersistentUIElement(string name) {
        CachedUIElement element = GetPersistentUIElement(name);
        element.cachedObject.GetComponent<PersistentUIElement>().Close();
    }

    private CachedUIElement GetPersistentUIElement(string name) {
        
        Debug.Log("Here 1");

        // First check for the object in the cache.
        for (int i = 0; i < persistentUIElementCache.Count; i++) {
            if (persistentUIElementCache[i].name == name) {
                return persistentUIElementCache[i];
            }
        }

        Debug.Log("Here 2");

        // Not in cache, so get the prefab an instantiate it...
        GameObject prefab = GetUIElement(name).prefab;
        GameObject newObj = GameObject.Instantiate(prefab, transform.position, Quaternion.identity, transform);

        // Create a cached entry...
        CachedUIElement newElement = new CachedUIElement();
        newElement.name = name;
        newElement.cachedObject = newObj;

        // Then cache it and return it!
        persistentUIElementCache.Add(newElement);
        return newElement;
    }

    private UIElementPrefab GetUIElement(string name) {
        for (int i = 0; i < uiElements.Length; i++) {
            if (uiElements[i].name == name)
                return uiElements[i];
        }

        Debug.LogWarning($"UI element with name '{name}' not found on GameObject called '{gameObject.name}'");
        return new UIElementPrefab();
    }
}
