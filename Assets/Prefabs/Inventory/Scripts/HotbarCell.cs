using UnityEngine;
using UnityEngine.EventSystems;

public class HotbarCell : MonoBehaviour, IPointerUpHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print(eventData.pointerCurrentRaycast);
    }
}
