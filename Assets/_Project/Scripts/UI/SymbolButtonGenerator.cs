using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolButtonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _symbolButtonPrefab;
    [SerializeField] private Transform _divineCodex;
    [SerializeField] private Transform _ritualCodex;
    [SerializeField] private Transform _estheticCodex;

    void Start()
    {
        SetupButton(SymbolManager.Instance.GetSymbolsOfType(SO_Symbols.SymbolType.Divine), _divineCodex);        foreach (SO_Symbols symbol in SymbolManager.Instance.GetSymbolsOfType(SO_Symbols.SymbolType.Divine))
        SetupButton(SymbolManager.Instance.GetSymbolsOfType(SO_Symbols.SymbolType.Ritual), _ritualCodex);
        SetupButton(SymbolManager.Instance.GetSymbolsOfType(SO_Symbols.SymbolType.Esthetic), _estheticCodex);
    }

    private void SetupButton(List<SO_Symbols> symbols, Transform parent)
    {
        foreach (SO_Symbols symbol in symbols)
        {
            GameObject symbolButton = Instantiate(_symbolButtonPrefab, parent);
            symbolButton.GetComponent<Button>().onClick.AddListener(() => CraftManager.Instance.SelectSymbol(symbol._id));
            symbolButton.GetComponent<S_HoverInfo>()._hoverHelperName = symbol._name;
            symbolButton.GetComponent<S_HoverInfo>()._hoverHelperText = symbol._description;
            
        }
    }
}
