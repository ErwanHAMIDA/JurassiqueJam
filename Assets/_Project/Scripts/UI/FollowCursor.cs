using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FollowCursor : MonoBehaviour
{
    //[SerializeField] private GameObject _firstView;
    //[SerializeField] private GameObject _secondView;
    //[SerializeField] private GameObject _swapLimitPoint;
    //[SerializeField] private GameObject _swapSpawnPoint;

    [SerializeField] private float movingThreshold = 5.0f;
    [SerializeField] private Transform _previsuZone;
    private bool _isOnRecipient = false;
    private bool _isClicking = false;
    private bool _isMoving = false;
    private Vector2 _startPosition;
    private Vector2 _lastPosition;

    [SerializeField] private AudioClip _movingSound;

    void Update()
    {
        if (!_isOnRecipient)
        {
            transform.position = Vector2.one * 1000.0f;   
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
            if (Vector2.Distance(_startPosition, Input.mousePosition) > movingThreshold)
            {
                _isMoving = true;
            }

            if (_isMoving)
            {
                Debug.Log("Move");
                CraftManager.Instance.MoveBaseTexture(Input.mousePosition.x - _lastPosition.x);
                S_SFXManager.Instance.PlayOneAtATimeSFXClip(_movingSound);
                _lastPosition = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_isMoving)
            {
                _isMoving = false;
            }
            else if (_isOnRecipient && transform.childCount > 0 && _isClicking)
            {
                Debug.Log("Place Symbol");
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
            Destroy(_previsuZone.GetChild(0).gameObject);
        }

        GameObject newSymbol = Instantiate(symbol, transform.position, Quaternion.identity, transform);
        Instantiate(symbol, _previsuZone.position, Quaternion.identity, _previsuZone);

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
            Destroy(_previsuZone.GetChild(0).gameObject);
        }
    }

    public void ChangeScale(float scale)
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).localScale = Vector3.one * scale;
            _previsuZone.GetChild(0).localScale = Vector3.one * scale;
        }
    }

    public void ChangeRotation(float rotation)
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).eulerAngles = new Vector3 (0,0,rotation);
            _previsuZone.GetChild(0).eulerAngles = new Vector3 (0,0,rotation);
        }
    }

    public void ChangeState(bool onRecipient)
    {
        _isOnRecipient = onRecipient;
    }
}
