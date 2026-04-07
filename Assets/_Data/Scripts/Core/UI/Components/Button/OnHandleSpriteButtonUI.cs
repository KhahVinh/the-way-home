using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHandleSpriteButtonUI : MonoBehaviour, IPointerDownHandler,
    IPointerUpHandler
{
    public Image render;
    public Sprite down;
    public Sprite up;
    public RectTransform rect;
    public float pressOffSet = 5f;

    public void OnPointerDown(PointerEventData eventData)
    {
        render.sprite = down;
        Vector2 current = rect.sizeDelta;
        rect.sizeDelta = new Vector2(current.x, current.y - pressOffSet);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        render.sprite = up;
        Vector2 current = rect.sizeDelta;
        rect.sizeDelta = new Vector2(current.x, current.y + pressOffSet);
    }
}
