using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class S_GameStateManager : MonoBehaviour
{

    public static S_GameStateManager Instance { get; private set; }

    [SerializeField] private GameObject _hubMenuUI;
    [SerializeField] private GameObject _craftMenuUI;
    [SerializeField] private GameObject _craftMenuBG;
    [SerializeField] private GameObject _itemControlUI;
    [SerializeField] private GameObject _backButtonUI;
    [SerializeField] private GameObject _recipientUI;
    [SerializeField] private GameObject _panelDialogUI;
    [SerializeField] private GameObject _endGameDialogUI;
    
    public enum GameState
    {
        PAUSEMENU,
        SELECTCLIENT,
        CLIENTSPAWN,
        WORKSHOPOVERVIEW,
        ITEMCRAFTING,
        ITEMDELIVERY,
        END
    }

    private GameState _previousGameState;
    public GameState Current;

    public UnityEvent<int> OnStateEnter;
    public UnityEvent<int> OnStateExit;

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
        OnStateExit?.Invoke((int)Current);
        
        _previousGameState = (GameState)state;
        
        switch (state)
        {
            case (int)GameState.SELECTCLIENT:
                _hubMenuUI.SetActive(true);
                _craftMenuUI.SetActive(false);
                _panelDialogUI.SetActive(false);
                break;
            case (int)GameState.CLIENTSPAWN:
                _panelDialogUI.SetActive(true);
                break;
            case (int)GameState.WORKSHOPOVERVIEW:
                _endGameDialogUI.SetActive(false);
                _hubMenuUI.SetActive(false);
                _craftMenuUI.SetActive(true);
                _craftMenuBG.GetComponent<Image>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                _itemControlUI.SetActive(true);
                _recipientUI.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                _backButtonUI.SetActive(false);
                break;
            case (int)GameState.ITEMCRAFTING:
                _craftMenuBG.GetComponent<Image>().color = new Vector4(0.33f, 0.33f, 0.33f, 1.0f);
                _itemControlUI.SetActive(false);
                _recipientUI.transform.localScale = Vector3.one;
                _backButtonUI.SetActive(true);
                break;
            case (int)GameState.ITEMDELIVERY:
                _craftMenuUI.SetActive(false);
                _hubMenuUI.SetActive(true);
                break;
            case (int)GameState.END:
                _endGameDialogUI.SetActive(true);
                
                break;
        }
        
        Current = (GameState)state;
        
        OnStateEnter?.Invoke((int)Current);
    }

    public void ChangeState(GameState state)
    {
        ChangeState((int)state);
    }
}