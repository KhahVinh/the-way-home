using TMPro;
using UnityEngine;

public class SetValueButtonUI : MonoBehaviour
{
    public TMP_Text text;

    public void SetText(string value)
    {
        text.text = value;
    }
}
