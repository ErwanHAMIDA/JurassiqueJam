using UnityEngine;
using UnityEngine.UI;

public class FollowCursor : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movingThreshold = 5.0f;

    [Header("Preview")]
    [SerializeField] private Transform _previsuZone;
    [SerializeField] private Transform _imagePrevisu;
    [SerializeField] private Vector3 _minScale = new(0.5f, 0.5f, 0.5f);
    [SerializeField] private Vector3 _maxScale = new(2.0f, 2.0f, 2.0f);
    [SerializeField] private float _slowSizeRatio = 0.33f;

    [Header("Audio")]
    [SerializeField] private AudioClip _movingSound;

    private bool _isOnRecipient = false;
    private bool _isClicking = false;
    private bool _isMoving = false;

    private Vector2 _startPosition;
    private Vector2 _lastPosition;

    private GameObject _currentSymbol;
    private GameObject _currentPreview;

    private void Update()
    {
        HandleCursorFollow();
        HandleInput();
    }

    private void HandleCursorFollow()
    {
        if (!_isOnRecipient)
        {
            transform.position = Vector2.one * 1000f;
        }
        else
        {
            transform.position = Input.mousePosition;
        }
    }

    private void HandleInput()
    {
        // Mouse down
        if (Input.GetMouseButtonDown(0) && _isOnRecipient)
        {
            _isClicking = true;
            _isMoving = false;

            _startPosition = Input.mousePosition;
            _lastPosition = Input.mousePosition;
        }

        // Mouse hold
        if (Input.GetMouseButton(0) && _isClicking)
        {
            if (!_isMoving &&
                Vector2.Distance(_startPosition, Input.mousePosition) > movingThreshold)
            {
                _isMoving = true;
            }

            if (_isMoving)
            {
                float deltaX = Input.mousePosition.x - _lastPosition.x;

                CraftManager.Instance.MoveBaseTexture(deltaX);

                if (_movingSound != null)
                {
                    S_SFXManager.Instance.PlayOneAtATimeSFXClip(_movingSound, 1.5f);
                }

                _lastPosition = Input.mousePosition;
            }
        }

        // Mouse up
        if (Input.GetMouseButtonUp(0))
        {
            if (!_isMoving &&
                _isOnRecipient &&
                _isClicking &&
                _currentSymbol != null)
            {
                CraftManager.Instance.PlaceSymbol(
                    Input.mousePosition,
                    _currentSymbol.transform.localScale,
                    _currentSymbol.transform.rotation
                );
            }

            ResetInteractionState();
        }
    }

    private void ResetInteractionState()
    {
        _isClicking = false;
        _isMoving = false;
    }

    public void ChangeSymbol(GameObject symbol)
    {
        ResetInteractionState();
        ResetSymbol();

        _currentSymbol = Instantiate(
            symbol,
            transform.position,
            Quaternion.identity,
            transform
        );

        SetupTransparentImages(_currentSymbol, 0.5f);

        if (_currentSymbol.TryGetComponent<Image>(out Image image))
        {
            if (_imagePrevisu.TryGetComponent<Image>(out Image previewImage))
            {
                previewImage.sprite = image.sprite;
                previewImage.color = Color.white;
            }
        }
    }

    public void ResetSymbol()
    {
        if (_currentSymbol != null)
        {
            Destroy(_currentSymbol);
            _currentSymbol = null;
        }
    }

    public void ChangeScale(float scale)
    {
        ResetInteractionState();

        if (_currentSymbol != null)
        {
            Vector3 newScale = Vector3.one * scale;

            _currentSymbol.transform.localScale = newScale;
        }

        if (_imagePrevisu != null)
        {
            Vector3 newScale = Vector3.one * scale * _slowSizeRatio;

            // .x because we don't care which one because the base is Vector(1, 1, 1) and the scale is even so x = y = z
            if (newScale.x > _maxScale.x)
                newScale = _maxScale; //max amount for visibility
            else if (newScale.x < _minScale.x)
                newScale = _minScale; //minimum amount for visibility

            _imagePrevisu.transform.localScale = newScale;
        }
    }

    public void ChangeRotation(float rotation)
    {
        ResetInteractionState();

        Vector3 rot = new Vector3(0, 0, rotation);

        if (_currentSymbol != null)
        {
            _currentSymbol.transform.eulerAngles = rot;
        }

        if (_currentPreview != null)
        {
            _currentPreview.transform.eulerAngles = rot;
        }
    }

    public void ChangeState(bool onRecipient)
    {
        _isOnRecipient = onRecipient;
    }

    private void SetupTransparentImages(GameObject obj, float alpha)
    {
        if (obj == null) return;

        Image[] images = obj.GetComponentsInChildren<Image>(true);

        foreach (Image img in images)
        {
            Color c = img.color;
            c.a = alpha;
            img.color = c;
        }
    }
}