using UnityEngine;

[CreateAssetMenu(fileName = "SO_Symbols", menuName = "Scriptable Objects/SO_Symbols")]
public class SO_Symbols : ScriptableObject
{
    public string _name;
    public int _id;
    public Sprite _sprite;
    public enum SymbolType
    {
        Gems,
        Triangle,
        Snake,
        LifeTree
    }
}
