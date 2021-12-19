using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IObject : MonoBehaviour
{
    public new string name;
    public string description;
    public GameObject prefab;
    public int stackable = 1;

    [Header("Posicion en la mano")]
    [Tooltip("Puede estar en la mano o no")]
    public bool inMano = false;
    public Vector3 posicionMano;
    public Quaternion rotacionMano;
    private Transform mano;

    [Tooltip("Es un arma o no")]
    public bool IsArma = false;

    [Header("inventario")]
    public Image icon;

    [Header("Crafteo")]
    public bool iscrafteable = false;
    public List<IQuerry> crafteo;

    private void Start()
    {
        if (inMano)
        {
            mano.position = posicionMano;
            mano.rotation = rotacionMano;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.TryGetComponent<IInventory>(out IInventory inventario))
        {
            if (inventario.Add(this))  Destroy(gameObject);
            else print("toco pero no se agrego");
        }

    }
}


[System.Serializable]
public class IQuerry
{
    public string name;
    public int quantity;

    public IQuerry(string name, int quantity)
    {
        this.name = name;
        this.quantity = quantity;
    }
}
[System.Serializable]
public class ISlot
{
    public IObject obj { set; get; }
    public int quantity { set; get; }

    public GameObject prefab;

    public ISlot(IObject item, int cantidad)
    {
        obj = item;
        quantity = cantidad;
    }
  
}
