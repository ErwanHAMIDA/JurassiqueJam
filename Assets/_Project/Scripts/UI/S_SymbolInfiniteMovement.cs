using UnityEngine;
using UnityEngine.Video;

public class S_SymbolInfiniteMovement : MonoBehaviour
{
    private float _minimumRotation;
    private float _maximumRotation;
    private RectTransform _rectTransform;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _minimumRotation = CraftManager.Instance._baseTexture.parent.GetComponent<RectTransform>().position.x - CraftManager.Instance._maximumRotation;
        _maximumRotation = CraftManager.Instance._baseTexture.parent.GetComponent<RectTransform>().position.x + CraftManager.Instance._maximumRotation;
    }

    void Update()
    {
        if (Screen.width < 100) return;
        if (_rectTransform.position.x < _minimumRotation * (Screen.width / 1920.0f))
        { 
            _rectTransform.position = new Vector3(_maximumRotation * (Screen.width / 1920.0f) + (_rectTransform.position.x - _minimumRotation * (Screen.width / 1920.0f)) , _rectTransform.position.y, _rectTransform.position.z);
        }
        else if (_rectTransform.position.x > _maximumRotation * (Screen.width / 1920.0f))
        {
            _rectTransform.position = new Vector3(_minimumRotation * (Screen.width / 1920.0f) + (_rectTransform.position.x - _maximumRotation * (Screen.width / 1920.0f)), _rectTransform.position.y, _rectTransform.position.z);
        }
    }
}
