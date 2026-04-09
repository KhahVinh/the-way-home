using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_ItemControl", menuName = "UI/ItemControl", order = 0)]
public class ItemControlSO : ScriptableObject
{
    public Sprite Image;
    public string Name;
    public List<string> ListGuideText = new List<string>();

    public string GetKeyFunction(int index)
    {
        return (string)this.ListGuideText[index].Split(',')[0];
    }

    public string GetGuideText(int index)
    {
        return (string)this.ListGuideText[index].Split(',')[1];
    }
}
