using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_HoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string _hoverHelperName;
    public string _hoverHelperText;
    [SerializeField] GameObject _textPanel;
    [SerializeField] TextMeshProUGUI _textMeshName;
    [SerializeField] TextMeshProUGUI _textMesh;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _textPanel.SetActive(true);
        _textMesh.gameObject.SetActive(true);
    }

    void Update()
    {
        if (_textPanel.activeSelf)
        {
            _textPanel.transform.position = Input.mousePosition;
        }
    }    

    public void OnPointerExit(PointerEventData eventData)
    {
        _textPanel.SetActive(false);
        _textMesh.gameObject.SetActive(false);
    }

    void Start()
    {
        _textMeshName.text = _hoverHelperName;
        _textMesh.text = _hoverHelperText;
    }
}
