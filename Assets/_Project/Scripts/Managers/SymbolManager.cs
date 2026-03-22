using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolManager : MonoBehaviour
{
    public static SymbolManager Instance { get; private set; }
    
    public SO_Symbols[] _symbolList;
    [SerializeField] List<Button> _buttonList;
    [SerializeField] Transform _containerSpawnPoint;
    
    private void Awake() 
    {         
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        if (_buttonList.Count != _symbolList.Length) throw new ArgumentException("The quantity button or symbol number are not the same", nameof(_buttonList));
        
        for (int i = 0; i < _buttonList.Count; i++)
        {
            if (_symbolList[i]._isAvailable)
                _buttonList[i].interactable = true;
            else
                _buttonList[i].interactable = false;
        }
    }
    public SO_Symbols GetSymbolById(int id)
    {
        for (int i = 0; i < _symbolList.Length; i++)
        {
            if (_symbolList[i]._id == id)
            {
                return _symbolList[i];
            }
        }

        return null;
    }

    public void UnlockSymbols(int id)
    {
        _buttonList[id].interactable = true;
    }

    public void ResetSymbolContainer(GameObject container)
    {
        Destroy(container);
        //Instantiate(SymbolContainer, _containerSpawnPoint)
    }

    public List<SO_Symbols> GetSymbolsOfType(SO_Symbols.SymbolType type)
    {
        List<SO_Symbols> allSymbolsOfType = new List<SO_Symbols>();

        foreach (SO_Symbols symbol in _symbolList)
        {
            if (symbol._symbolType == type)
            {
                allSymbolsOfType.Add(symbol);
            }
        }

        return allSymbolsOfType;
    }
}
