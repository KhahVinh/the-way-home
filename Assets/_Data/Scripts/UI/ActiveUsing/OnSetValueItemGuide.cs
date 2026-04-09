using TMPro;
using UnityEngine;

public class OnSetValueItemGuide : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _keyFunction;
    [SerializeField]
    private TMP_Text _guideText;

    public void SetData(string keyFunc, string guideText)
    {
        _keyFunction.text = keyFunc;
        _guideText.text = guideText;
    }
}
