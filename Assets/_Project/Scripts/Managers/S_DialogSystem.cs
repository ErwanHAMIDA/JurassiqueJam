using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_DialogSystem : MonoBehaviour
{
    [SerializeField] List<string> _allSentencesList;
    [SerializeField] TextMeshProUGUI _dialogText;

    private int _currentIndex = 0;

    private void Start()
    {
        UpdateDialog();
    }

    public void NextDialog()
    {
        if (_currentIndex >= _allSentencesList.Count - 1) return;

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
        _dialogText.text = _allSentencesList[_currentIndex];
    }
}
