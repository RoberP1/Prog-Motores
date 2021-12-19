using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler,IBeginDragHandler,IEndDragHandler,IPointerUpHandler
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform parent;
    private ISlot SelectedSlot;
    private IInventory inv;
    


    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("mainCanvas").GetComponent<Canvas>();
        inv = FindObjectOfType<IInventory>();
        SelectedSlot = GetComponentInParent<ISlotUI>().slot;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        SelectedSlot = GetComponentInParent<ISlotUI>().slot;
        canvasGroup.blocksRaycasts = false;
        inv.selectedSlot = SelectedSlot;
        print(SelectedSlot.obj.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
        transform.SetParent(canvas.transform) ;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parent);
        rectTransform.anchoredPosition = new Vector2(0, 0);
        inv.selectedSlot = null;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        parent = transform.parent;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parent);
        rectTransform.anchoredPosition = new Vector2(0, 0);
    }
}
