using TMPro;
using UnityEngine;

public class S_HelpManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _helpTextMesh;

    [Header("References")]
    [SerializeField] private S_DialogSystem _dialogSystem;

    public void UpdateText()
    {
        _helpTextMesh.text = _dialogSystem.GetDialog();
    }
}
