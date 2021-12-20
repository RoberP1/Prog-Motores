using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recurso : MonoBehaviour
{
    public GameObject recurso;
    public float Vida;
    public int randomLootmin;
    public int randomLootmax;

    public void RecibirDamage(float damage)
    {
        Vida -= damage;
        if (Vida <= 0)
        {
            Destroy(gameObject);
            for (int i = 0; i < Random.Range(randomLootmin, randomLootmax); i++)
            {
                Instantiate(recurso, transform.position + new Vector3(Random.Range(-4, 4), 5, Random.Range(-4, 4)), transform.rotation);
            }
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
            RecibirDamage(obj.damage);
        }
    }
}
