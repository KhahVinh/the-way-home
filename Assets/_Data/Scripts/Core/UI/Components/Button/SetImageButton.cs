using System;
using ObserverPattern;
using TMPro;
using UnityEngine;

public class SetImageButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _pass;
    [SerializeField]
    private GameObject _lock;
    [SerializeField]
    private GameObject _current;

    public int LevelIndex;
    public TMP_Text text;
    public struct data
    {
        public int currentLevel;
        public int levelSelected;
    }
    public void SetText(string value)
    {
        text.text = value;
    }
    private Action<object> e_updateImage;
    void OnEnable()
    {
        e_updateImage = (currentLevel) => UpdateImage((data)currentLevel);

        EventDispatcherExtension.RegisterListener(this, EventID.LoadLevel, e_updateImage);
    }

    void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.LoadLevel, e_updateImage);
    }

    private void UpdateImage(data levelData)
    {
        int currentLevel = levelData.currentLevel;
        int levelSelected = levelData.levelSelected;
        if (currentLevel < LevelIndex)
        {
            _pass.SetActive(false);
            _lock.SetActive(true);
            _current.SetActive(false);
        }
        else if (currentLevel == LevelIndex)
        {
            _pass.SetActive(false);
            _lock.SetActive(false);
            _current.SetActive(true);
        }
        else
        {
            _pass.SetActive(true);
            _lock.SetActive(false);
            _current.SetActive(false);
        }

        if (levelSelected == LevelIndex)
        {
            this.TextEffect();
        }
        else
        {
            text.color = new Color32(255, 255, 255, 255);
        }
    }

    private void TextEffect()
    {
        // text.color = "A6A6A6".HexToColor();
        text.color = new Color32(154, 154, 154, 255);
    }
}
