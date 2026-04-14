using UnityEngine;
using UnityEngine.UI;

public class S_ClickedAllowed : MonoBehaviour
{
    [SerializeField] private Image _image;

    private void Start()
    {
        _image.alphaHitTestMinimumThreshold = 0.1f;
    }
}
