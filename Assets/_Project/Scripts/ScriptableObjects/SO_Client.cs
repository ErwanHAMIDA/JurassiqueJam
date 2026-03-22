using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Client", menuName = "Scriptable Objects/SO_Client")]
public class SO_Client : ScriptableObject
{
    public string _name;
    public Sprite _sprite;

    public SO_Symbols.Civilization _clientOrigin;

    public SO_Symbols.SymbolType[] _typesPreferences;
    public int[] _numberNeededType;

    public SO_Symbols.SymbolTag[] _tagsPreferences;
    public int[] _numberNeededTag;
}
