using UnityEngine;
using UnityEngine.UI;

public class S_CarvedSymbol : MonoBehaviour
{
    [SerializeField] private Image _texture;

    void Start () 
	{
		GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
	}

    public void ChangeTexture()
    {
        if (CraftManager.Instance._selectedMaterial == null) return;

        _texture.enabled = true;
        _texture.sprite = CraftManager.Instance._selectedMaterial;
        Destroy(this);
    }
}
