using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolManager : MonoBehaviour
{
    public static SymbolManager Instance { get; private set; }
    
    public SO_Symbols[] _symbolList;
    [SerializeField] List<Button> _buttonList;
    
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
}
