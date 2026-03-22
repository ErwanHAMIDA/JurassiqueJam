using UnityEngine;
using UnityEngine.UI;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private GameObject _firstView;
    [SerializeField] private GameObject _secondView;
    [SerializeField] private GameObject _swapLimitPoint;
    [SerializeField] private GameObject _swapSpawnPoint;

    private bool _isOnRecipient = false;
    private bool _isClicking = false;
    private bool _isMoving = false;
    private RectTransform _rectTransform;
    private Vector2 _startPosition;
    private Vector2 _lastPosition;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!_isOnRecipient)
        {
            _rectTransform.localPosition = Vector3.zero;   
        }
        else
        {
            transform.position = Input.mousePosition;
        }
        
        // Place Symbol
        if (Input.GetMouseButtonDown(0) && _isOnRecipient)
        {
            _isClicking = true;
            _startPosition = Input.mousePosition;
            _lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && _isClicking)
        {
            if (Vector2.Distance(_startPosition, Input.mousePosition) > 0.1f)
            {
                _isMoving = true;
            }

            if (_isMoving)
            {
                CraftManager.Instance.MoveBaseTexture(Input.mousePosition.x - _lastPosition.x);
                _lastPosition = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Place Symbol");
            if (_isMoving)
            {
                _isMoving = false;
            }
            else if (_isOnRecipient)
            {
                CraftManager.Instance.PlaceSymbol(Input.mousePosition, transform.GetChild(0).localScale, transform.GetChild(0).rotation);
            }

            _isClicking = false;
        }
    }

    public void ChangeSymbol(GameObject symbol)
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        GameObject newSymbol = Instantiate(symbol, transform.position, Quaternion.identity, transform);

        if (newSymbol.TryGetComponent<Image>(out Image image))
        {
            image.color = new Color (image.color.r, image.color.g, image.color.b, 0.5f);
        }

        for (int i = 0; i < newSymbol.transform.childCount; i++)
        {
            if (newSymbol.transform.GetChild(i).gameObject.TryGetComponent<Image>(out Image imageChild))
            {
                imageChild.color = new Color (imageChild.color.r, imageChild.color.g, imageChild.color.b, 0.5f);
            }
        }
    }

    public void ResetSymbol()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void ChangeScale(float scale)
    {
        transform.GetChild(0).localScale = Vector3.one * scale;
    }

    public void ChangeRotation(float rotation)
    {
        transform.GetChild(0).eulerAngles = new Vector3 (0,0,rotation);
    }

    public void ChangeState(bool onRecipient)
    {
        _isOnRecipient = onRecipient;
    }
}
