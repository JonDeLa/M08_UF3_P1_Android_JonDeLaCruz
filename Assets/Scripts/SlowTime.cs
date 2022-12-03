using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    public float slowQuamtity = 0.05f;
    public float slowTime = 4f;
    void Update()
    {
        Time.timeScale += (1f / slowTime) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
    public void slowMotion()
    {
        Time.timeScale = slowQuamtity;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
