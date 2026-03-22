using UnityEngine;
using UnityEngine.UIElements;

public class S_GameStateManager : MonoBehaviour
{

    public static S_GameStateManager Instance { get; private set; }

    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _hubMenuUI;
    [SerializeField] private GameObject _craftMenuUI;
    [SerializeField] private GameObject _itemControlUI;
    [SerializeField] private GameObject _backButtonUI;
    [SerializeField] private GameObject _recipientUI;
    [SerializeField] private GameObject _panelDialogUI;

    public enum GameState
    {
        PAUSEMENU,
        SELECTCLIENT,
        CLIENTSPAWN,
        WORKSHOPOVERVIEW,
        ITEMCRAFTING,
        ITEMDELIVERY,
        REWARD
    }

    private GameState _previousGameState;
    public GameState Current;

    public void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }

    public void ChangeState(int state)
    {
        _previousGameState = (GameState)state;
        
        switch (state)
        {
            case (int)GameState.PAUSEMENU:
                _pauseMenuUI.SetActive(true);

                break;
            case (int)GameState.SELECTCLIENT:
                _hubMenuUI.SetActive(true);
                _craftMenuUI.SetActive(false);
                _panelDialogUI.SetActive(false);
                break;
            case (int)GameState.CLIENTSPAWN:
                _panelDialogUI.SetActive(true);
                break;
            case (int)GameState.WORKSHOPOVERVIEW:
                _hubMenuUI.SetActive(false);
                _craftMenuUI.SetActive(true);
                _itemControlUI.SetActive(true);
                _recipientUI.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                _backButtonUI.SetActive(false);
                break;
            case (int)GameState.ITEMCRAFTING:
                _itemControlUI.SetActive(false);
                _recipientUI.transform.localScale = Vector3.one;
                _backButtonUI.SetActive(true);
                break;
            case (int)GameState.ITEMDELIVERY:
                _craftMenuUI.SetActive(false);
                _hubMenuUI.SetActive(true);
                break;
            case (int)GameState.REWARD:
                break;
        }

        Current = (GameState)state;
    }
}