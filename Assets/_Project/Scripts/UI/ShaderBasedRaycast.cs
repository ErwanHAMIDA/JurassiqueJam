using UnityEngine;
using UnityEngine.UI;

public class ShaderBasedRaycast : MonoBehaviour, ICanvasRaycastFilter
{
    [SerializeField] private Image image;
    [SerializeField] private bool isInverted;

    [Range(0, 1)]
    [SerializeField] private float alphaThreshold = 0.1f;

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        RectTransform rect = image.rectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, sp, eventCamera, out Vector2 localPoint);

        // UV 0–1 dans le rect UI
        Vector2 uv = new Vector2(
            (localPoint.x + rect.rect.width * 0.5f) / rect.rect.width,
            (localPoint.y + rect.rect.height * 0.5f) / rect.rect.height
        );

        // --- MAIN TEXTURE (depuis le sprite) ---
        Sprite sprite = image.sprite;
        Texture2D mainTex = sprite.texture;

        // Correction UV pour atlas / sprite découpé
        Rect spriteRect = sprite.textureRect;

        uv = new Vector2(
            (spriteRect.x + uv.x * spriteRect.width) / mainTex.width,
            (spriteRect.y + uv.y * spriteRect.height) / mainTex.height
        );

        Color main = mainTex.GetPixelBilinear(uv.x, uv.y);

        // --- MASK (depuis le material) ---
        Texture2D maskTex = image.material.GetTexture("_Mask") as Texture2D;

        // Sécurité si pas de mask
        float maskValue = 1f;
        if (maskTex != null)
        {
            Color mask = maskTex.GetPixelBilinear(uv.x, uv.y);
            maskValue = isInverted ? (1f - mask.r) : mask.r;
        }

        float alpha = main.a * maskValue;

        return alpha > alphaThreshold;
    }
}