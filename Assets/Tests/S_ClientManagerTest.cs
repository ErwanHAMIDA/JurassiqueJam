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

        SO_Client testClient02 = ScriptableObject.CreateInstance<SO_Client>();
        testClient02._typesPreferences = new[] { SO_Symbols.SymbolType.Divine };
        testClient02._numberNeededType = new[] { 6 };
        testClient02._tagsPreferences = new [] {SO_Symbols.SymbolTag.Cosmic };
        testClient02._numberNeededTag = new [] { 6 };
        AssetDatabase.CreateAsset(testClient02, "Assets/_Project/ScriptableObjects/Clients/TEST02.asset");

        SO_Client testClient03 = ScriptableObject.CreateInstance<SO_Client>();
        testClient03._typesPreferences = new[] { SO_Symbols.SymbolType.Divine, SO_Symbols.SymbolType.Esthetic };
        testClient03._numberNeededType = new[] { 1, 1 };
        testClient03._tagsPreferences = new[] { SO_Symbols.SymbolTag.Cosmic, SO_Symbols.SymbolTag.Moon };
        testClient03._numberNeededTag = new[] { 1, 1 };
        AssetDatabase.CreateAsset(testClient03, "Assets/_Project/ScriptableObjects/Clients/TEST03.asset");
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        AssetDatabase.DeleteAsset("Assets/_Project/ScriptableObjects/Clients/TEST01.asset");
        AssetDatabase.DeleteAsset("Assets/_Project/ScriptableObjects/Clients/TEST02.asset");
        AssetDatabase.DeleteAsset("Assets/_Project/ScriptableObjects/Clients/TEST03.asset");
    }
    
    [Test]
    [
        // A client that receives no symbols is Unhappy.
        TestCase(S_ClientManager.ClientSatisfaction.Unhappy, "TEST01", new string[] {}),
        // A client that receives less than a third (rounded down) of the required symbols is Sad.
        TestCase(S_ClientManager.ClientSatisfaction.Sad, "TEST02", new [] { "ID00_Etoile" }),
        // A client that only need one type and one tag is Joyful when a symbol validates both.
        TestCase(S_ClientManager.ClientSatisfaction.Joyful, "TEST01", new [] { "ID00_Etoile" }),
        // A client that receives three quarters of the required types and tags is Happy
        TestCase(S_ClientManager.ClientSatisfaction.Happy, "TEST03", new [] { "ID00_Etoile", "ID16_Triangle" }),
        // The actual clients in the game can be brought to Joyful state from symbols within the game.
        TestCase(S_ClientManager.ClientSatisfaction.Joyful, "ID00_SumWoman", new [] { "ID01_Lune", "ID04_Oeil" }),
        TestCase(S_ClientManager.ClientSatisfaction.Joyful, "ID01_SumMan", new [] { "ID05_Rosace", "ID00_Etoile", "ID00_Etoile", "ID16_Triangle", "ID16_Triangle", "ID16_Triangle", "ID16_Triangle", "ID16_Triangle" }),
        TestCase(S_ClientManager.ClientSatisfaction.Joyful, "ID02_AkkWoman", new [] { "ID13_Lion", "ID13_Lion" }),
        TestCase(S_ClientManager.ClientSatisfaction.Joyful, "ID03_AkkMan", new string[] { "ID02_Soleil", "ID12_Guerrier", "ID14_Ecaille", "ID14_Ecaille", "ID14_Ecaille" }),
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
}
