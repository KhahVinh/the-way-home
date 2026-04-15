using System.Collections;
using TMPro;
using UnityEngine;

public class DayTextUI : MonoBehaviour
{
    public DayNightSystem _system;
    [SerializeField]
    private TMP_Text _dayText;

    private void Start()
    {
        _system.OnDayStart += UpdateDayText;
    }

    private void UpdateDayText()
    {
        StartCoroutine(ChangeDay());
    }

    private IEnumerator ChangeDay()
    {
        _dayText.text = "";
        yield return new WaitForSeconds(1f);
        _dayText.text = "Day " + _system.DayCount;
    }
}
