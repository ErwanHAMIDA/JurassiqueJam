using UnityEngine;


public class S_DDOL : MonoBehaviour
{
    [SerializeField] private string _tagName;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(_tagName);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}