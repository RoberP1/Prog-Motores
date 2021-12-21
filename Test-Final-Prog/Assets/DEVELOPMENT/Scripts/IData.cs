using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IData : MonoBehaviour
{
    public IInventory inventario;
    public string[] inventorysave;
    [SerializeField] public insave inventory;

    void Start()
    {
        inventario = FindObjectOfType<IInventory>();
        inventory.invsave = new SlotSave[15];

        //LoadJSON();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SaveJSON();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadJSON();
        }

    }
    public void SaveJSON()
    {
        print("saving");
        inventory.invsave = new SlotSave[15];
        for (int i = 0; i < 15; i++)
        {
            /*
            string obj = "";
            if (inventario.inventory[i].quantity != 0)
            {
                obj = JsonUtility.ToJson(inventario.inventory[i].obj.prefab);
            }
            if (obj == "") obj = "null";
            print(obj);
            int quantity = inventario.inventory[i].quantity;
            print(quantity);
            inventory.invsave[i] = new SlotSave(obj,quantity);
            print(JsonUtility.ToJson(inventory.invsave[i]));
            */
            print(inventario.inventory[i]);
            string obj ="";
            if(inventario.inventory[i].quantity >0) obj = JsonUtility.ToJson(inventario.inventory[i].obj.prefab);
            print(obj);
            inventorysave[i] = obj;
            PlayerPrefs.SetString("saveData"+i, JsonUtility.ToJson(inventorysave));

        }
        

        /*
        string saveData = JsonUtility.ToJson(inventory);
        print(saveData);
        PlayerPrefs.SetString("saveData", saveData);
        */
    }
    public void LoadJSON()
    {
        print("cargar inv");

        for (int i = 0; i < inventorysave.Length; i++)
        {
            string data = PlayerPrefs.GetString("saveData"+i);
            //inventario.inventory[i] = JsonUtility.FromJson<ISlot>(data);
            print(JsonUtility.FromJson<GameObject>(data));
        }
        inventario.UpdateHotbar();
        inventario.invUI.UpdateInv();


        /*
        print("cargar inv");
        print(JsonUtility.ToJson(inventory));
        string data = PlayerPrefs.GetString("saveData");


        
        //if (data != "") inventory = JsonUtility.FromJson<SlotSave[]>(data);
        for (int i = 0; i < inventory.invsave.Length; i++)
        {
            if (inventory.invsave[i].quantity > 0)
            {
                inventario.inventory[i].obj = JsonUtility.FromJson<GameObject>(inventory.invsave[i].obj).GetComponent<IObject>();
                inventario.inventory[i].quantity = inventory.invsave[i].quantity;
                inventario.inventory[i].prefab = JsonUtility.FromJson<GameObject>(inventory.invsave[i].obj);
                print(inventario.inventory[i].obj.name);
            }
        }
        
        */
    }
}
[System.Serializable]
public class SlotSave
{
    public string obj;
    public int quantity;


    public SlotSave(string obj, int quantity)
    {
        this.obj = obj;
        this.quantity = quantity;
    }
}
[System.Serializable]
public class insave
{
    public SlotSave[] invsave = new SlotSave[15];
}


