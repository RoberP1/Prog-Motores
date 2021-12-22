using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collecionables : MonoBehaviour
{
    private Manager manager;
    void Start() => manager = FindObjectOfType<Manager>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.EncontrarCol();
            Destroy(gameObject);
        }
    }
}
