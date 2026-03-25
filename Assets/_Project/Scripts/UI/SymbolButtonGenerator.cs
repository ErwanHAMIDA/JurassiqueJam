using System.Collections.Generic;
using TMPro;
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
        SetupButton(SymbolManager.Instance.GetSymbolsOfType(SO_Symbols.SymbolType.Divine), _divineCodex);
        SetupButton(SymbolManager.Instance.GetSymbolsOfType(SO_Symbols.SymbolType.Ritual), _ritualCodex);
        SetupButton(SymbolManager.Instance.GetSymbolsOfType(SO_Symbols.SymbolType.Esthetic), _estheticCodex);
    }

    private void SetupButton(List<SO_Symbols> symbols, Transform parent)
    {
        foreach (SO_Symbols symbol in symbols)
        {
            //GameObject parent2 = Instantiate(_symbolButtonPrefab, parent);

            //GameObject childImage = Instantiate(symbol._symbolPrefab, parent2.transform);
            //childImage.gameObject.transform.localScale /= 2;

            GameObject symbolButton = Instantiate(_symbolButtonPrefab, parent);
            symbolButton.GetComponent<Button>().onClick.AddListener(() => CraftManager.Instance.SelectSymbol(symbol._id));
            symbolButton.GetComponent<S_HoverInfo>()._hoverHelperName = symbol._name;
            symbolButton.GetComponent<S_HoverInfo>()._hoverHelperText = symbol._description;
            symbolButton.GetComponent<Image>().sprite = symbol._symbolSprite;

            
        }
    }
}
