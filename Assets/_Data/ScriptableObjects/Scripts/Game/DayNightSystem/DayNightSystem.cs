using System;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    [Header("Time Settings (seconds)")]
    public float dayDuration = 360f;   // 6 phút
    public float nightDuration = 240f; // 4 phút

    public DayState currentState = DayState.Day;
    public int DayCount = 1;
    private float timer;

    // Event cho hệ khác subscribe
    public event Action OnDayStart;
    public event Action OnNightStart;

    void Start()
    {
        timer = dayDuration;
        OnDayStart?.Invoke();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SwitchState();
        }
    }

    void SwitchState()
    {
        if (currentState == DayState.Day)
        {
            currentState = DayState.Night;
            timer = nightDuration;

            Debug.Log("Night started");
            OnNightStart?.Invoke();
        }
        else
        {
            currentState = DayState.Day;
            timer = dayDuration;

            DayCount++; // 👉 TĂNG NGÀY Ở ĐÂY

            OnDayStart?.Invoke();
        }
    }

    public float GetTimePercent()
    {
        float duration = currentState == DayState.Day ? dayDuration : nightDuration;
        return 1f - (timer / duration);
    }
}
