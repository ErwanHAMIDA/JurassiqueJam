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

    private int _clientId;
    private int _preferenceCheckIndex = 0;

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

    public void CompareItem()
    {
        int currentSymbolNumber = SymbolManager.Instance._symbolList.Length;
        int clientPreferences = _clientList[_clientId]._preferences.Count;

        if (currentSymbolNumber < clientPreferences) return;

        int index = 0;

        for (int i = 0; i < currentSymbolNumber; i++)
        {
            for (int j = 0; j < clientPreferences; j++)
            {
                if (_clientList[_clientId]._preferences[i] == SymbolManager.Instance.GetSymbolById(j))
                {
                    Debug.Log("OKAY");
                    return;
                }
            }
        }

        foreach (SO_Symbols symbols in SymbolManager.Instance._symbolList)
        {
            if (_clientList[_clientId]._preferences[_preferenceCheckIndex] == SymbolManager.Instance.GetSymbolById(index)) Debug.Log("OKAY");

            index++;
        }
    }
}
