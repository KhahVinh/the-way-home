using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : ButtonBase
{
    [SerializeField]
    private AudioClip _clipButtonClick;

    protected override void OnClick()
    {
        AudioManager.Instance.PlaySound(_clipButtonClick);
    }
}
