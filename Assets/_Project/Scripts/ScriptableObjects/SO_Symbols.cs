using UnityEngine;

[CreateAssetMenu(fileName = "SO_Symbols", menuName = "Scriptable Objects/SO_Symbols")]
public class SO_Symbols : ScriptableObject
{
    public int _id;
    public string _name;
    public string _description;
    public GameObject _symbolPrefab;

    public SymbolType _symbolType;
    public Civilization[] _symbolOrigin;

    public SymbolTag[] _symbolTags;

    public enum SymbolType
    {
        Divine,
        Ritual,
        Esthetic
    }

    public enum Civilization
    {
        Sumerian,
        Akkadian,
        Assyrian
    }
    public enum SymbolTag
    {
        Animal,
        Vegetal,
        Cosmic,
        Strength,
        Fertility,
        Moon,
        Sun

    }
}
