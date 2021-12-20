using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ISlotUI : MonoBehaviour, IDropHandler
{

    //public ISlot selectedSlot;
    public Text itemName;
    public Text itemDescription;
    public Text itemQuantity;
    public Image icon;

    public ISlot slot = new ISlot(null, 0);
    public int index;

    private IInventory inv;

    private void Start()
    {

        inv = FindObjectOfType<IInventory>();
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (slot.quantity == 0 || slot.obj.name != inv.selectedSlot.obj.name)
            inv.CambiarLugar(index);
    }

}

