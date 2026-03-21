using UnityEngine;

public class S_GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuUI;
    public enum GameState
    {
        PAUSEMENU,
        SELECTCLIENT,
        CLIENTSPAWN,
        ITEMDELIVERY,
        REWARD
    }

    private GameState _currentGameState;

    private bool _isPaused = true;

    public void Start()
    {
        _currentGameState = GameState.PAUSEMENU;
    }
    public void ChangeState(int state)
    {
        _currentGameState = (GameState)state;

        switch (_currentGameState)
        {
            case (GameState.PAUSEMENU):
                break;
            case (GameState.SELECTCLIENT):
                break;
            case (GameState.CLIENTSPAWN):
                break;
            case (GameState.ITEMDELIVERY):
                break;
            case (GameState.REWARD):
                break;
        }

        //_previousGameState = (GameState)state;
    }

    
}