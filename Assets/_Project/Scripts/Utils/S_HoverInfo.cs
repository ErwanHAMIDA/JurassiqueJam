using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_HoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string _hoverHelperName;
    public string _hoverHelperText;
    [SerializeField] GameObject _textPanel;
    [SerializeField] TextMeshProUGUI _textMeshName;
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

    void OnEnable()
    {
        _textMeshName.text = _hoverHelperName;
        _textMesh.text = _hoverHelperText;
        _textPanel.gameObject.transform.position = gameObject.transform.position + _offset; // Could be with a emptyGameObject placed on scene, may be easier to place ?
    }
}
