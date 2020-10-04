using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private float prevTimeScale;
    private float targetTimeScale;
    private float lerpTime;
    private float lerpTimeDuration;
    

    private void Awake()
    {
        if (instance != null) {
            GameObject.DestroyImmediate(this.gameObject);
        }
        instance = this;
    }

    private void Update()
    {

        if (lerpTime > lerpTimeDuration)
        {
            lerpTime = lerpTimeDuration;
            Time.timeScale = targetTimeScale;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            lerpTimeDuration = 0;
        }
        else if (lerpTimeDuration != 0)
        {
            LerpTime();
        }
    }

    public void SetTimeScale(float timeScale, float transitionDuration = 0.0f)
    {
        Debug.Log("Setting TimeScale");
        if (transitionDuration == 0)
        {
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = timeScale * 0.02f;
            return;
        }
        prevTimeScale = Time.timeScale;
        targetTimeScale = timeScale;
        lerpTimeDuration = transitionDuration;
        lerpTime = 0;
        // TODO: Implement the transition duration.
    }

    private void LerpTime()
    {
        Time.timeScale = Mathf.Lerp(prevTimeScale, targetTimeScale, lerpTime / lerpTimeDuration);
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        lerpTime += Time.unscaledDeltaTime;
    }
}
