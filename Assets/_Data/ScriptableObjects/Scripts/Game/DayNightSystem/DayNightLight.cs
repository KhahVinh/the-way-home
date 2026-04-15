using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightLight : MonoBehaviour
{
    public DayNightSystem system;
    public Light2D globalLight;

    [Header("Intensity")]
    public float dayIntensity = 1f;
    public float nightIntensity = 0.3f;

    void Update()
    {
        float t = system.GetTimePercent();

        if (system.currentState == DayState.Day)
        {
            globalLight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, t);
        }
        else
        {
            globalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, t);
        }
    }
}
