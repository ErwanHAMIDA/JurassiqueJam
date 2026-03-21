using UnityEngine;

public class S_SnapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _snapPointsArray;

    private void Start()
    {
        foreach(var snapPoint in _snapPointsArray)
        {
            Debug.Log(snapPoint.transform.position);
        }
    }
}
