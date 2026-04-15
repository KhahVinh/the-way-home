using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightController : MonoBehaviour
{
    public Light2D playerLight;
    public DayNightSystem system;

    void Start()
    {
        system.OnNightStart += TurnOnLight;
        system.OnDayStart += TurnOffLight;
    }

    void TurnOnLight()
    {
        playerLight.enabled = true;
    }

    void TurnOffLight()
    {
        playerLight.enabled = false;
    }
}
