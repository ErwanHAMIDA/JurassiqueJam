using UnityEngine;

public class S_DDOL : MonoBehaviour
{
    private void Awake()
    {
        if (this != null)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }
}