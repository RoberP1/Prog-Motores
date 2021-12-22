using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogata : MonoBehaviour
{
    private IInventory inv;
    private AudioSource sound;
    [SerializeField] private IObject carne;
    private bool esta = false;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        inv = FindObjectOfType<IInventory>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && esta && inv.ObjInMano != null && inv.ObjInMano.GetComponent<IObject>().name == "Raw Meat")
        {
            inv.sacaruno(inv.slotinmano);
            if (!inv.Add(carne)) inv.DropObject(carne);
            sound.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) esta = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) esta = false;
    }
}
