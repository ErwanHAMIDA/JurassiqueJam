using UnityEngine;
using UnityEngine.UI;

public class FollowCursor : MonoBehaviour
{
    private bool _isOnRecipient = false;
    private RectTransform _rectTransform;

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
        
    }

    public void ChangeSymbol(GameObject symbol)
    {
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

    public void ChangeScale(float scale)
    {
        transform.GetChild(0).localScale = Vector3.one * scale;
    }

    public void ChangeRotation(float rotation)
    {
        transform.GetChild(0).eulerAngles = new Vector3 (0,0,rotation);
    }

    void OnDisable()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void ChangeState(bool onRecipient)
    {
        _isOnRecipient = onRecipient;
    }
}
