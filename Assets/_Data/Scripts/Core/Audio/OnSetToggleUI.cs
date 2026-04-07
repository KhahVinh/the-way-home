using UnityEngine;
using UnityEngine.UI;

public class OnSetToggleUI : ButtonBase
{
    public Sprite _image1;
    public Sprite _image2;
    public Image _image;
    protected override void OnClick()
    {
        if (_image.sprite == _image1)
        {
            _image.sprite = _image2;
        }
        else
        {
            _image.sprite = _image1;
        }
    }
}
