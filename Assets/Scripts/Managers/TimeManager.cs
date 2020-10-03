using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Singleton Pattern
    private static TimeManager _instance;

    public static TimeManager Instance
    {
        get
        {
            //Create logic to create the instance
            if(_instance == null)
            {
                GameObject go = new GameObject("Time Manager");
                go.AddComponent<TimeManager>();
            }

            return _instance;
        }
    }
    #endregion

    private void Awake()
    {
        _instance = this;
    }

    private float defaultTimeScale;
    public bool slowMotion { get; set; }
    public float slowMotionSpeed { get; set; }

    private void Start()
    {
        defaultTimeScale = Time.fixedDeltaTime;
        slowMotion = false;
        slowMotionSpeed = .4f;
    }

    public void ScaleTime()
    {
        if (!slowMotion)
        {
            Time.timeScale = slowMotionSpeed;
            Time.fixedDeltaTime = slowMotionSpeed * 0.02f;
            slowMotion = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = this.defaultTimeScale;
            slowMotion = false;
        }
    }
}
