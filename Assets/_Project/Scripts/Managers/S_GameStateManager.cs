using UnityEngine;

public class S_GameStateManager : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _hubMenuUI;
    [SerializeField] private GameObject _craftMenuUI;
    [SerializeField] private GameObject _craftManager;

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
    private GameState _currentGameState;

    private bool _isPaused = true;

    public void Start()
    {
        _currentGameState = GameState.PAUSEMENU;
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
                break;
            case (int)GameState.CLIENTSPAWN:
                
                break;
            case (int)GameState.WORKSHOPOVERVIEW:
                _hubMenuUI.SetActive(false);
                _craftMenuUI.SetActive(true);
                _craftManager.SetActive(false);
                break;
            case (int)GameState.ITEMCRAFTING:
                _craftManager.SetActive(true);
                break;
            case (int)GameState.ITEMDELIVERY:
                _craftMenuUI.SetActive(false);
                _hubMenuUI.SetActive(true);
                _craftManager.SetActive(false);
                break;
            case (int)GameState.REWARD:
                break;
        }

        _currentGameState = (GameState)state;
    }

    
}