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


    [Tooltip("Es un arma o no")]
    public bool IsArma = false;
    public BoxCollider AttackArea;
    public float damage;

    [Header("inventario")]
    public Image icon;

    [Header("Crafteo")]
    public bool iscrafteable = false;
    public List<IQuerry> crafteo;

    public string uso;

    private void OnTriggerEnter(Collider other)
    {
        if(!inMano && other.TryGetComponent<IInventory>(out IInventory inventario) && inventario.Add(this))
            Destroy(gameObject);
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
