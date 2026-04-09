using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MyBehaviour
{
    public UIController _uiController;
    public SetInfoActiveUsing _setInfo;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // _uiController.ChangeState(UIController.UIState.LEVELS);
            _setInfo.SetInfo(0);
            _setInfo.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            // _uiController.ChangeState(UIController.UIState.LEVELS);
            _setInfo.SetInfo(1);
            _setInfo.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            // _uiController.ChangeState(UIController.UIState.LEVELS);
            _setInfo.SetInfo(2);
            _setInfo.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _setInfo.gameObject.SetActive(false);
        }
    }
}
