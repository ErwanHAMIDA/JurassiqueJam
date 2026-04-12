using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_CursorOnRecipient : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private FollowCursor _followCursor;
    [SerializeField] private GameObject _zoomedUI;
    [SerializeField] private GameObject _commandReminderButton;

    private bool _isTheFirstTimeClickedOnSymbol = true;

    void Start () 
	{
		GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(S_GameStateManager.Instance.Current);
        if (S_GameStateManager.Instance.Current != S_GameStateManager.GameState.ITEMCRAFTING)
        {
            S_GameStateManager.Instance.ChangeState((int)S_GameStateManager.GameState.ITEMCRAFTING);
            _followCursor.ChangeState(true);
            
            _zoomedUI.SetActive(true);
            _commandReminderButton.SetActive(true);
            CraftManager.Instance.CheckOnboarding(2);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (S_GameStateManager.Instance.Current == S_GameStateManager.GameState.ITEMCRAFTING)
        {
            _followCursor.ChangeState(true);        
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (S_GameStateManager.Instance.Current == S_GameStateManager.GameState.ITEMCRAFTING)
        {
            _followCursor.ChangeState(false);
        }
    }
}
