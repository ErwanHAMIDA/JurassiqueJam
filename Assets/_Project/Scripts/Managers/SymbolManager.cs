using UnityEngine;

public class SymbolManager : MonoBehaviour
{
    public static SymbolManager Instance { get; private set; }
    
    public SO_Symbols[] _symbolList;

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
}
