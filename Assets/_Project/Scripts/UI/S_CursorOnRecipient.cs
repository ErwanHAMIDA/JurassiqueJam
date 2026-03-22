using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_CursorOnRecipient : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private FollowCursor _followCursor;

	void Start () 
	{
		GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (S_GameStateManager.Instance.Current == S_GameStateManager.GameState.WORKSHOPOVERVIEW)
        {
            S_GameStateManager.Instance.ChangeState((int)S_GameStateManager.GameState.ITEMCRAFTING);
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
