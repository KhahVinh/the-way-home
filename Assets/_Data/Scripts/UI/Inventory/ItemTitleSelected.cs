using TMPro;
using UnityEngine;

public class ItemTitleSelected : MyBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTMPText();
    }

    protected virtual void LoadTMPText()
    {
        if (_text != null) return;
        _text = transform.GetComponent<TMP_Text>();
    }

    public void ChangeText(string content = "")
    {
        if (_text == null)
        {
            Debug.LogWarning("TMP_Text component is null", this);
            return;
        }
        _text.text = content.Trim();
    }
}
