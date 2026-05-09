using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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
    [CanBeNull] public SO_Client CurrentClient => _clientId >= 0 && _clientId < _clientList.Count ? _clientList[_clientId] : null;

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
        List<SO_Symbols> placedSymbols = CraftManager.Instance.GetPlacedSymbols().Select(symbol => SymbolManager.Instance.GetSymbolById(symbol.Id)).ToList();

        return CalculateClientSatisfaction(placedSymbols, _clientList[_clientId]);
    }

    public static ClientSatisfaction CalculateClientSatisfaction(List<SO_Symbols> placedSymbols, SO_Client client)
    {
        if (placedSymbols.Count == 0) return ClientSatisfaction.Sad;

        float totalPreferredSymbols = 0.0f;

        foreach (var symbol in placedSymbols)
        {
            if (symbol._symbolOrigin.Contains(client._clientOrigin) &&
                (client._typesPreferences.Contains(symbol._symbolType) ||
                 symbol._symbolTags.Any(tag => client._tagsPreferences.Contains(tag))))
            {
                totalPreferredSymbols++;
            }
        }

        float satisfactionRatio = totalPreferredSymbols / placedSymbols.Count;

        if (satisfactionRatio <= client._permissiveRatio) return ClientSatisfaction.Sad;

        int score = 0;
        int clientPreferencesTypes = client._typesPreferences.Length;
        int clientPreferencesTags = client._tagsPreferences.Length;

        int maxScore = clientPreferencesTypes + clientPreferencesTags;

        // Check client prefered types
        for (int i = 0; i < clientPreferencesTypes; i++)
        {
            int count = placedSymbols.Count(s =>
                s._symbolType == client._typesPreferences[i] &&
                s._symbolOrigin.Contains(client._clientOrigin));

            if (count >= client._numberNeededType[i])
                score++;
            else if (count < client._numberNeededType[i] / 3f)
                score--;
        }
        
        // Check client prefered tags
        for (int i = 0; i < clientPreferencesTags; i++)
        {
            int count = placedSymbols.Count(s =>
                s._symbolTags.Contains(client._tagsPreferences[i]) &&
                s._symbolOrigin.Contains(client._clientOrigin));

            if (count >= client._numberNeededTag[i])
                score++;
            else if (count < client._numberNeededTag[i] / 3f)
                score--;
        }

        float finalScore = score * satisfactionRatio;

        if (finalScore >= maxScore)
            return ClientSatisfaction.Joyful;
        else if (finalScore >= maxScore * 0.5f)
            return ClientSatisfaction.Happy;
        else if (finalScore >= 0)
            return ClientSatisfaction.Unhappy;
        else
            return ClientSatisfaction.Sad;
    }

    public bool AreAllClientsCompleted()
    {
        var completedClients = _clientList.Sum(client => client.Satisfaction == ClientSatisfaction.Joyful ? 1 : 0);
        return completedClients >= _clientList.Count;
    }
    
    public void HandleStateEnter(int state)
    {
        switch (state)
        {
            case (int)S_GameStateManager.GameState.ITEMDELIVERY:
                if (CurrentClient is not null) CurrentClient.Satisfaction = CompareItem();

                _clientId = -1;
                
                if (AreAllClientsCompleted()) S_GameStateManager.Instance.ChangeState(S_GameStateManager.GameState.END);
                else S_GameStateManager.Instance.ChangeState((int)S_GameStateManager.GameState.SELECTCLIENT);
                
                break;
        }
    }
}
