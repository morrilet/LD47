using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private void Awake()
    {
        if (instance != null) {
            GameObject.DestroyImmediate(this.gameObject);
        }
        instance = this;
    }

    public void SetTimeScale(float timeScale, float transitionDuration = 0.0f)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = timeScale * 0.02f;
    }
}
