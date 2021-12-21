using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class IObject : MonoBehaviour
{
    public new string name;
    public string description;
    public GameObject prefab;
    public int stackable = 1;
    public bool recogible;

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

    public string uso;

    private void OnTriggerEnter(Collider other)
    {
        if(recogible && !inMano && other.TryGetComponent<IInventory>(out IInventory inventario) && inventario.Add(this))
            Destroy(gameObject);
    }
}



