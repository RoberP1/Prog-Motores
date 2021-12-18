using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IInventory : MonoBehaviour
{
    public ISlot[] inventory = new ISlot[15];
    public ISlot[] hotbar = new ISlot[5];

    public GameObject UIinv;
    public ISlot selectedSlot;

    private IInventoryUI invUI;
    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            inventory[i] = new ISlot(null,0);
        }
        UIinv.SetActive(false);
        invUI = FindObjectOfType<IInventoryUI>();
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIinv.SetActive(!UIinv.activeSelf);
            //Time.timeScale = (UIinv.activeSelf) ? 0 : 1;
            Cursor.lockState =  (UIinv.activeSelf) ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    //los primeros 5 slots del inventario son de la hotbar
    void UpdateHotbar()
    {
        for (int i = 0; i < hotbar.Length; i++)
        {
            hotbar[i] = inventory[i];
            
            //Debug.Log("pos: " +i +" nombre: "+inventory[i].obj.name + " cantidad: " + inventory[i].quantity);
        }
    }

    int BuscarVacio() //busca el primer slot vacio, si no encuentra devuelve -1
    {
        
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].quantity == 0) return i;
        }
        Debug.Log("inventario lleno");
        return -1;
    }
    public bool Add(IObject item)
    {
      
        bool agregado = false;
      
        foreach (ISlot s in inventory)
        {
            if (s.quantity != 0 && s.obj.name == item.name && item.stackable > s.quantity)
            {
                s.quantity++;
                agregado = true;
                break;
            }
        }
        
        if (!agregado)
        {
            int i = BuscarVacio();
            if (i != -1)
            {
                inventory[i] = new ISlot(item, 1);
                agregado = true;
            }
        }
        UpdateHotbar();
        invUI.UpdateInv();

        return agregado;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IObject>(out IObject item))
        {
            if (Add(item))
            {
                Destroy(other.gameObject);
            }
        }
    }
    public int BuscarSlot(ISlot slot)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == slot) return i;
        }
        Debug.Log("no se encontro el slot");
        return -1;
    } 
    public void CambiarLugar( int dest)
    {
        int i = BuscarSlot(selectedSlot);
        inventory[dest] = inventory[i];
        inventory[i] = new ISlot(null, 0);
        invUI.UpdateInv();
    }
}
