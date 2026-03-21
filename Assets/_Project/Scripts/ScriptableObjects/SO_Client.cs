using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Client", menuName = "Scriptable Objects/SO_Client")]
public class SO_Client : ScriptableObject
{
    public string _name;
    public Sprite _sprite;
    public List<SO_Symbols> _preferences;
}
