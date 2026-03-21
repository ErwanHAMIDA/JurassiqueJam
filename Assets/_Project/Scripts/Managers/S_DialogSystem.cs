using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_DialogSystem : MonoBehaviour
{
    [SerializeField] List<SO_ClientDialog> _allclientSentencesList;
    [SerializeField] TextMeshProUGUI _dialogText;
    [SerializeField] int _selectedClientIndex = 0; //temporary here, will be choosen on clientManager

    private int _currentIndex = 0;

    private void Start()
    {
        UpdateDialog();
    }

    public void NextDialog()
    {
        if (_currentIndex >= _allclientSentencesList[_selectedClientIndex]._clientSentences.Count - 1) return;

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
        _dialogText.text = _allclientSentencesList[_selectedClientIndex]._clientSentences[_currentIndex];
    }
}
