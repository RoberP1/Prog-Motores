
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private ISlotUI[] inv = new ISlotUI[15];

    

    private IInventory inventory;
    private void Start()
    {
        inventory = FindObjectOfType<IInventory>();
        for (int i = 0; i < inv.Length; i++)
        {
            inv[i].index = i;
        }
    }


    void Update()
    {
        
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

            }
            else
            {
                inv[i].slot = new ISlot(null,0);
                inv[i].icon.gameObject.SetActive(false);
                inv[i].itemQuantity.gameObject.SetActive(false);
            }
            
        }
    }
}
