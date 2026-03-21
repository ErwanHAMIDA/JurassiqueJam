using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_DialogSystem : MonoBehaviour
{
    [SerializeField] List<SO_ClientDialog> _allclientSentencesList;
    [SerializeField] TextMeshProUGUI _dialogText;
    [SerializeField] S_ClientManager _clientManager;

    private int _currentIndex = 0;

    public void NextDialog()
    {
        if (_currentIndex >= _allclientSentencesList[_clientManager.GetCurrentClientID()]._clientSentences.Count - 1) return;

        _currentIndex++;
        UpdateDialog();
    }

    public void PreviousDialog()
    {
        if (_currentIndex <= 0) return;

        _currentIndex--;
        UpdateDialog();
    }

    public void UpdateDialog()
    {
        _dialogText.text = _allclientSentencesList[_clientManager.GetCurrentClientID()]._clientSentences[_currentIndex];
    }
}
