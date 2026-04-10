using UnityEngine;

/// <summary>
/// Class used for match ui with target ui via rect transform
/// Handle only UI Canvas
/// </summary>
public static class MatchUI
{
    /// <summary>
    /// Method handle match ui
    /// </summary>
    /// <param name="target">Your element UI Canvas want to process</param>
    /// <param name="source">Rect transform want to attach</param>
    /// <param name="canvas"></param>
    public static void HandleMatchUI(RectTransform target, RectTransform source, Canvas canvas, Vector2 offset)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            source.position
        );

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPoint,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out Vector2 localPoint
        );
        target.localPosition = localPoint + offset;
    }
}
