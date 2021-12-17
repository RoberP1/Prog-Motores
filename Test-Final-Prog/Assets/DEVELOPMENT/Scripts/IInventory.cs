using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IInventory : MonoBehaviour
{
    public ISlot[] inventory = new ISlot[15];
    public ISlot[] hotbar = new ISlot[5];
    public ISlotUI slotUIPrefab;
    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            inventory[i] = new ISlot(new IObject(), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
            print(inventory[0]);
            print(inventory[0].obj);
            print(inventory[0].obj.stackable);
            if (inventory[i].obj.name == null) return i;
        }
        Debug.Log("inventario lleno");
        return -1;
    }
    public bool Add(IObject item)
    {
      
        bool agregado = false;
      
        foreach (ISlot s in inventory)
        {
            
           
            if (s.obj.name != null && s.obj.name == item.name && item.stackable > s.quantity)
            {
                print("hola");
                s.quantity++;
                print(s.quantity);
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

        return agregado;
    }
}
