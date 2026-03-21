using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_CursorOnRecipient : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private FollowCursor _followCursor;

	void Start () 
	{
		GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
	}
    public void OnPointerEnter(PointerEventData eventData)
    {
        _followCursor.ChangeState(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _followCursor.ChangeState(false);
    }
}
