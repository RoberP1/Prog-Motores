using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recurso : MonoBehaviour
{
    public AudioSource reproductor;
    public GameObject destruir;
    public AudioClip[] sonidos;
    public GameObject recurso;
    public float Vida;
    public int randomLootmin;
    public int randomLootmax;
    public string recolector;


    public void RecibirDamage(float damage)
    {
        reproductor.clip = sonidos[0];
        reproductor.Play();
        Vida -= damage;
        if (Vida <= 0)
        {

            
            for (int i = 0; i < Random.Range(randomLootmin, randomLootmax); i++)
            {
                Instantiate(recurso, transform.position + new Vector3(Random.Range(-4, 4), 5, Random.Range(-4, 4)), transform.rotation);
            }
            AudioSource dead = Instantiate(destruir).GetComponent<AudioSource>();
            dead.clip = sonidos[1];
            dead.Play();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IObject obj = other.GetComponentInParent<IObject>();
        if (other.CompareTag("golpemano"))
        {
            
            RecibirDamage(20);


        }
        else if (obj != null && obj.inMano)
        {
            if (obj.name == recolector)
            {
                RecibirDamage(obj.damage);


            }
            else
            {

                RecibirDamage(obj.damage / 2);
                
            }
            
        }
    }
}
