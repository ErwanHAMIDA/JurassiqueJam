using UnityEngine;
using UnityEngine.UI;

public class FollowCursor : MonoBehaviour
{
    void Update()
    {
        transform.position = Input.mousePosition;
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

    void OnDisable()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
