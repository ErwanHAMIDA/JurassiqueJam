using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "SO_ClientDialog", menuName = "Scriptable Objects/SO_ClientDialog")]
public class SO_ClientDialog : ScriptableObject
{
    public List<string> _clientSentences;
}
