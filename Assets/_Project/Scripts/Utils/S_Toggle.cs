using UnityEngine;

public class S_Toggle : MonoBehaviour
{
    public void Toggle(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}
