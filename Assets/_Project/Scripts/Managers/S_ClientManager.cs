using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class S_ClientManager : MonoBehaviour
{
    [SerializeField] List<SO_Client> _clientList;
    [SerializeField] GameObject _clientDialogPanel;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] GameObject _clientImage;

    public static S_ClientManager Instance { get; private set; }

    public enum ClientSatisfaction
    {
        Joyful,
        Happy,
        Unhappy,
        Sad
    }

    private int _clientId;

    public void SelectClient(int index)
    {
        if (index >= _clientList.Count || index < 0) return;

        Instantiate(_clientList[index], _spawnPoint);

        _clientImage.SetActive(true);
        _clientDialogPanel.gameObject.SetActive(true);

        _clientImage.GetComponent<Image>().sprite = _clientList[index]._sprite;
        _clientId = index;
    }

    public int GetCurrentClientID()
    {
        return _clientId;
    }

    public ClientSatisfaction CompareItem()
    {
        List<PlacedSymbol> placedSymbols = CraftManager.Instance.GetPlacedSymbols();
        int currentSymbolNumber = placedSymbols.Count;
        int clientPreferencesTypes = _clientList[_clientId]._typesPreferences.Length;
        int clientPreferencesTags = _clientList[_clientId]._tagsPreferences.Length;

        int score = 0;

        int maxScore = clientPreferencesTypes + clientPreferencesTags;

        // Check client preferences
        int[] currentTypes = new int[currentSymbolNumber];
        int[] currentTags = new int[currentSymbolNumber];

        // Check client prefered types
        for (int i = 0; i < _clientList[_clientId]._typesPreferences.Length; i++)
        {
            for (int j = 0; j < currentSymbolNumber; j++)
            {
                SO_Symbols currentSymbol = SymbolManager.Instance.GetSymbolById(placedSymbols[i].Id);
                if (currentSymbol._symbolType == _clientList[_clientId]._typesPreferences[i] && currentSymbol._symbolOrigin.Contains(_clientList[_clientId]._clientOrigin))
                {
                    currentTypes[i]++;
                }
            }
        }

        for (int i = 0; i < currentTypes.Length; i++)
        {
            if (currentTypes[i] > _clientList[_clientId]._numberNeededType[i])
                score++;
            else if (currentTypes[i] < _clientList[_clientId]._numberNeededType[i] / 3)
            {
                score--;
            }
        }

        // Check client prefered types
        for (int i = 0; i < _clientList[_clientId]._typesPreferences.Length; i++)
        {
            for (int j = 0; j < currentSymbolNumber; j++)
            {
                SO_Symbols currentSymbol = SymbolManager.Instance.GetSymbolById(placedSymbols[i].Id);
                if (currentSymbol._symbolTags.Contains(_clientList[_clientId]._tagsPreferences[i]) && currentSymbol._symbolOrigin.Contains(_clientList[_clientId]._clientOrigin))
                {
                    currentTags[i]++;
                }
            }
        }

        for (int i = 0; i < currentTags.Length; i++)
        {
            if (currentTags[i] > _clientList[_clientId]._numberNeededTag[i])
                score++;
            else if (currentTags[i] < _clientList[_clientId]._numberNeededTag[i] / 3)
            {
                score--;
            }            
        }

        if (score >= maxScore)
            return ClientSatisfaction.Joyful;
        else if (score >= maxScore * 0.75f)
            return ClientSatisfaction.Happy;
        else if (score >= 0)
            return ClientSatisfaction.Unhappy;
        else
            return ClientSatisfaction.Sad;
    }
}
