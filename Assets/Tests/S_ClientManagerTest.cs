using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class S_ClientManagerTest
{
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SO_Client testClient01 = ScriptableObject.CreateInstance<SO_Client>();
        testClient01._typesPreferences = new [] { SO_Symbols.SymbolType.Divine };
        testClient01._numberNeededType = new [] { 1 };
        testClient01._tagsPreferences = new [] { SO_Symbols.SymbolTag.Cosmic };
        testClient01._numberNeededTag = new [] { 1 };
        AssetDatabase.CreateAsset(testClient01, "Assets/_Project/ScriptableObjects/Clients/TEST01.asset");
    }
    
    // A Test behaves as an ordinary method
    [Test]
    [
        TestCase( S_ClientManager.ClientSatisfaction.Joyful, "TEST01", new [] { "ID00_Etoile" } )
    ]
    public void CalculateClientSatisfactionShouldReturnCorrectSatisfactionValue(
        S_ClientManager.ClientSatisfaction expectedSatisfaction, 
        string clientName,
        string[] assetNames
    )
    {
        SO_Client client = AssetDatabase.LoadAssetAtPath<SO_Client>($"Assets/_Project/ScriptableObjects/Clients/{clientName}.asset");
        List<SO_Symbols> placedSymbols = new List<SO_Symbols>(
            assetNames.Select(
                name => AssetDatabase.LoadAssetAtPath<SO_Symbols>($"Assets/_Project/ScriptableObjects/Symbols/{name}.asset")
            )
        );

        var satisfaction = S_ClientManager.CalculateClientSatisfaction(placedSymbols, client);
        
        Assert.AreEqual(expectedSatisfaction, satisfaction);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        AssetDatabase.DeleteAsset("Assets/_Project/ScriptableObjects/Clients/TEST01.asset");
    }
}
