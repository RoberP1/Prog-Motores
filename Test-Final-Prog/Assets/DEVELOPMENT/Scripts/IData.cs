using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IData : MonoBehaviour
{
    public IInventory inventario;
    public SlotSave[] inventory = new SlotSave[15];
    void Start()
    {
        inventario = FindObjectOfType<IInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        print(inventory.Length);
    }
    public void SaveJSON()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            string obj = JsonUtility.ToJson(inventario.inventory[i].obj);
            print(obj);
            int quantity = inventario.inventory[i].quantity;
            print(quantity);
            inventory[i] = new SlotSave(obj,quantity);
        }
        string saveData = JsonUtility.ToJson(inventory);
        print(saveData);
        PlayerPrefs.SetString("saveData", saveData);
    }
    public void LoadJSON()
    {
        print("cargar inv");
        string data = PlayerPrefs.GetString("saveData");
        inventory = JsonUtility.FromJson<SlotSave[]>(data);
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].quantity > 0)
            {
                inventario.inventory[i].obj = JsonUtility.FromJson<IObject>(inventory[i].obj);
                inventario.inventory[i].quantity = inventory[i].quantity;
                inventario.inventory[i].prefab = inventario.inventory[i].obj.prefab;
            }
        }
    }
}
[System.Serializable]
public class SlotSave
{
    public string obj { set; get; }
    public int quantity { set; get; }

    public SlotSave(string item, int cantidad)
    {
        obj = item;
        quantity = cantidad;
    }

}

