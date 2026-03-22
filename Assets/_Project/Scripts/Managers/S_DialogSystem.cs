using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_DialogSystem : MonoBehaviour
{
    [SerializeField] List<SO_ClientDialog> _allclientSentencesList;
    [SerializeField] TextMeshProUGUI _dialogText;
    [SerializeField] S_ClientManager _clientManager;
    [SerializeField] GameObject _previousButton;
    [SerializeField] GameObject _nextButton;
    [SerializeField] GameObject _endDialogButton;
    [SerializeField] GameObject _dialogPanel;

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

        Debug.Log(_currentIndex);
        if (_currentIndex == 0)
        {
            _previousButton.gameObject.SetActive(false);
        }

        if (_currentIndex == _allclientSentencesList[_clientManager.GetCurrentClientID()]._clientSentences.Count - 1)
        {
            _nextButton.gameObject.SetActive(false);
            _endDialogButton.gameObject.SetActive(true);
        }
        else
        {
            _nextButton.gameObject.SetActive(true);
            _previousButton.gameObject.SetActive(true);
            _endDialogButton.gameObject.SetActive(false);
        }
    }

    public void EndDialog()
    {
        _dialogPanel.gameObject.SetActive(false);
    }
}
