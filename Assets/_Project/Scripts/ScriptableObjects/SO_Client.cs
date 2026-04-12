using System;
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

    [SerializeField]
    private S_ClientManager.ClientSatisfaction _satisfaction = S_ClientManager.ClientSatisfaction.Sad;
    public S_ClientManager.ClientSatisfaction Satisfaction
    {
        get => _satisfaction;
        set
        {
            Debug.Log(value);
            _satisfaction = value;

            OnSatisfactionChanged?.Invoke(_satisfaction);
        }
    }

    public event Action<S_ClientManager.ClientSatisfaction> OnSatisfactionChanged;

    public void OnValidate()
    {
        Satisfaction = _satisfaction;
    }
}
