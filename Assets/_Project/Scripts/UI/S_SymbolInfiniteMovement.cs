using UnityEngine;

public class S_SymbolInfiniteMovement : MonoBehaviour
{
    private float _minimumRotation;
    private float _maximumRotation;

    void Start()
    {
        _minimumRotation = CraftManager.Instance._baseTexture.parent.position.x - CraftManager.Instance._maximumRotation;
        _maximumRotation = CraftManager.Instance._baseTexture.parent.position.x + CraftManager.Instance._maximumRotation;
    }

    void Update()
    {
        if (transform.position.x < _minimumRotation)
        {
            transform.position = new Vector3(_maximumRotation + (transform.position.x - _minimumRotation) , transform.position.y, transform.position.z);
        }
        else if (transform.position.x > _maximumRotation)
        {
            transform.position = new Vector3(_minimumRotation + (transform.position.x - _maximumRotation), transform.position.y, transform.position.z);
        }
    }
}
