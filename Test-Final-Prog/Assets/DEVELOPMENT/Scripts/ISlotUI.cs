using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ISlotUI : MonoBehaviour, IDropHandler
{
    public ISlot slot = new ISlot(null,0);
    public int index;

    //public ISlot selectedSlot;
    public Text itemName;
    public Text itemDescription;
    public Text itemQuantity;
    public Image icon;

    private IInventory inv;
    private IInventoryUI invUI;
    private void Start()
    {
        inv = FindObjectOfType<IInventory>();
        invUI = FindObjectOfType<IInventoryUI>();
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        
        if (slot.quantity == 0 || slot.obj.name != inv.selectedSlot.obj.name)
        {
            //mover de lugar el slot
            inv.CambiarLugar(index);

        }
        else if (slot.obj.name == inv.selectedSlot.obj.name && slot.obj.stackable > inv.selectedSlot.quantity + slot.quantity)
        {
            //agregar cantidad al slot y borrar el slot seleccionado
        }
    }

}

