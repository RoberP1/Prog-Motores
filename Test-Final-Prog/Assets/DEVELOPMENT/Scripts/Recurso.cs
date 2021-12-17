using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    private GameObject yo; 
    public float health;

    void Start()
    {
        yo = gameObject;
    }

 
    void Update()
    {

    }
    public void RecibirDamage(float damage)
    {
        health -= damage;
        if (health <= 0) 
        {
            Destroy(yo);
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("golpemano"))
        {
            
            RecibirDamage(50);
        }
        if(other.CompareTag("golpearma"))
        {
            print("pego");
            RecibirDamage(100);
        }
    }
}
