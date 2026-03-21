using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ClientManager : MonoBehaviour
{
    [SerializeField] List<SO_Client> _clientList;
    [SerializeField] GameObject _clientDialogPanel;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] GameObject _clientImage;

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
}
