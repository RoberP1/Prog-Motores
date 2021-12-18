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
    private ISlotUI SlotUI;
    private ISlot SelectedSlot;
    private IInventory inv;
    


    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("mainCanvas").GetComponent<Canvas>();
        SlotUI = FindObjectOfType<ISlotUI>();
        inv = FindObjectOfType<IInventory>();
        SelectedSlot = GetComponentInParent<ISlotUI>().slot;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        SelectedSlot = GetComponentInParent<ISlotUI>().slot;
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
        inv.selectedSlot = SelectedSlot;
        print(SelectedSlot.obj.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
        transform.SetParent(canvas.transform) ;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("solte");
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parent);
        rectTransform.anchoredPosition = new Vector2(0, 0);
        inv.selectedSlot = null;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("hice click");
        parent = transform.parent;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parent);
        rectTransform.anchoredPosition = new Vector2(0, 0);
    }
}
