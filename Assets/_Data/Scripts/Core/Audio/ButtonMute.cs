using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class ButtonMute : ButtonBase
{
    public bool _mute = false;
    public Sprite _image1;
    public Sprite _image2;
    public Image _image;
    protected override void Start()
    {
        base.Start();
        _mute = PlayerPrefs.GetInt("mute", 0) == 0 ? false : true;
        _image.sprite = _mute ? _image1 : _image2;
    }
    protected override void OnClick()
    {
        _mute = !_mute;
        AudioManager.Instance._audioSFX.mute = _mute;
        PlayerPrefs.SetInt("mute", _mute ? 1 : 0);
        PlayerPrefs.Save();
    }
}
