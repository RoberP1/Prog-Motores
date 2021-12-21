using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogata : MonoBehaviour
{
    private IInventory inv;
    private AudioSource sound;
    [SerializeField] private IObject carne;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        inv = FindObjectOfType<IInventory>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && inv.ObjInMano != null && inv.ObjInMano.GetComponent<IObject>().name == "Raw Meat")
        {
            inv.sacaruno(inv.slotinmano);
            if (!inv.Add(carne)) inv.DropObject(carne);
            sound.Play();
        }
    }
}
