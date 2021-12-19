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

    [SerializeField]private Transform mano;
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
            Time.timeScale = (UIinv.activeSelf) ? 0 : 1;
            Cursor.lockState =  (UIinv.activeSelf) ? CursorLockMode.None : CursorLockMode.Locked;
        }
        if (!UIinv.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //checkear si tiene objeto hotbar 1
                    //si tiene ponerlo en la mano
                    //checkear si es  un arma
                        //si es arma cambiar modo de ataque
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {

            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {

            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {

            }
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
                inventory[i].prefab = Instantiate(inventory[i].obj.prefab,transform.localPosition + new Vector3(2,1,0) , transform.rotation,transform);
                inventory[i].prefab.SetActive(false);
                agregado = true;
            }
        }
        UpdateHotbar();
        invUI.UpdateInv();

        return agregado;
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
        ISlot tempslot = (inventory[dest].quantity == 0) ? new ISlot(null, 0) : inventory[dest];
        int i = BuscarSlot(selectedSlot);
        inventory[dest] = inventory[i];
        inventory[i] = tempslot;
        UpdateHotbar();
        invUI.UpdateInv();
    }
    public void DropSelected()
    {
        int i = BuscarSlot(selectedSlot);
        for (int j = 0; j < selectedSlot.quantity; j++)
        {
            Instantiate(selectedSlot.prefab, selectedSlot.prefab.transform.position + new Vector3(0,j/2,0), selectedSlot.prefab.transform.rotation).SetActive(true);
        }
        //inventory[i].prefab.SetActive(true);
        Destroy(inventory[i].prefab);
        
        //inventory[i].prefab.transform.SetParent(null);
        inventory[i] = new ISlot(null, 0);
        UpdateHotbar();
        invUI.UpdateInv();
    }
}
