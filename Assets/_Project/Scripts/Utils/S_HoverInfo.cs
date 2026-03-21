using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_HoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string _hoverHelperText;
    [SerializeField] GameObject _textPanel;
    [SerializeField] TextMeshProUGUI _textMesh;
    [SerializeField] Vector3 _offset;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _textPanel.SetActive(true);
        _textMesh.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _textPanel.SetActive(false);
        _textMesh.gameObject.SetActive(false);
    }

    void Start()
    {
        _textMesh.text = _hoverHelperText;
        _textPanel.gameObject.transform.position = gameObject.transform.position + _offset; // Could be with a emptyGameObject placed on scene, may be easier to place ?
    }
}
