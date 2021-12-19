using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recurso : MonoBehaviour
{
    public GameObject yo;
    
   
    
    public float Vida;
    void Start()
    {
        yo = gameObject;
    }

 
    void Update()
    {

    }
    public void RecibirDamage(float damage)
    {
        Vida -= damage;
        if (Vida <= 0) 
        {
  
            Destroy(yo);
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("golpemano"))//cambiar a trytogetcomponent<Iobject>(out Iobject arma) y si arma.isarma
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
