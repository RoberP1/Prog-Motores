
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IInventoryUI : MonoBehaviour
{
    [SerializeField] private ISlotUI[] inv = new ISlotUI[15];
    [SerializeField] public ISlotUI[] hotbar = new ISlotUI[5];

    private IInventory inventory;
    private void Start()
    {
        inventory = FindObjectOfType<IInventory>();
        for (int i = 0; i < inv.Length; i++) inv[i].index = i;
        hotbar[0].GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void UpdateInv()
    {
        for (int i = 0; i < inv.Length; i++)
        {
            if(inventory.inventory[i].quantity != 0)
            {
                inv[i].slot = inventory.inventory[i];
                inv[i].icon.sprite = inventory.inventory[i].obj.icon.sprite;
                inv[i].itemQuantity.text = "x" + inventory.inventory[i].quantity;
                inv[i].icon.gameObject.SetActive(true);
                inv[i].itemQuantity.gameObject.SetActive(true);
                if (i < hotbar.Length)
                {
                    hotbar[i].slot = inv[i].slot;
                    hotbar[i].slot = inventory.hotbar[i];
                    hotbar[i].icon.sprite = inventory.hotbar[i].obj.icon.sprite;
                    hotbar[i].itemQuantity.text = "x" + inventory.hotbar[i].quantity;
                    hotbar[i].icon.gameObject.SetActive(true);
                    hotbar[i].itemQuantity.gameObject.SetActive(true);
                }
            }
            else
            {
                inv[i].slot = new ISlot(null,0);
                inv[i].icon.gameObject.SetActive(false);
                inv[i].itemQuantity.gameObject.SetActive(false);
                if (i < hotbar.Length)
                {
                    hotbar[i].slot = new ISlot(null, 0);
                    hotbar[i].icon.gameObject.SetActive(false);
                    hotbar[i].itemQuantity.gameObject.SetActive(false);
                }
            }
        }
    }
}
