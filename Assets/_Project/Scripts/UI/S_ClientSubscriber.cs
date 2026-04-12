using UnityEngine;
using UnityEngine.Events;

public class S_ClientSubscriber : MonoBehaviour
{
    [SerializeField] private SO_Client _clientData;
    
    public UnityEvent OnSatisactionJoyful;
    public UnityEvent OnSatisfactionHappy;
    public UnityEvent OnSatisfactionUnhappy;
    public UnityEvent OnSatisfactionSad;

    public void Start()
    {
        _clientData.OnSatisfactionChanged += HandleSatisfactionChanged;
    }

    public void HandleSatisfactionChanged(S_ClientManager.ClientSatisfaction satisfaction)
    {
        switch (satisfaction)
        {
            case S_ClientManager.ClientSatisfaction.Joyful:
                OnSatisactionJoyful?.Invoke();
                break;
            case S_ClientManager.ClientSatisfaction.Happy:
                OnSatisfactionHappy?.Invoke();
                break;
            case S_ClientManager.ClientSatisfaction.Unhappy:
                OnSatisfactionUnhappy?.Invoke();
                break;
            case S_ClientManager.ClientSatisfaction.Sad:
                OnSatisfactionSad?.Invoke();
                break;
        }
    }

    public void OnDestroy()
    {
        _clientData.OnSatisfactionChanged -= HandleSatisfactionChanged;
    }
}
