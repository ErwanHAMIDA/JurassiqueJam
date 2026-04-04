using UnityEngine;
using UnityEngine.UI;

public class ShaderBasedRaycast : MonoBehaviour, ICanvasRaycastFilter
{
    [SerializeField] private Image image;
    [SerializeField] private Texture2D mainTex;
    [SerializeField] private Texture2D maskTex;
    [SerializeField] private bool isInverted;

    [Range(0, 1)]
    [SerializeField] private float alphaThreshold = 0.1f;

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        RectTransform rect = image.rectTransform;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, sp, eventCamera, out localPoint);

        Vector2 uv = new Vector2(
            (localPoint.x + rect.rect.width * 0.5f) / rect.rect.width,
            (localPoint.y + rect.rect.height * 0.5f) / rect.rect.height
        );

        Color main = mainTex.GetPixelBilinear(uv.x, uv.y);
        Color mask = maskTex.GetPixelBilinear(uv.x, uv.y);

        float alpha = main.a * (isInverted ? (1f - mask.r) : mask.r);

        return alpha > alphaThreshold;
    }
}