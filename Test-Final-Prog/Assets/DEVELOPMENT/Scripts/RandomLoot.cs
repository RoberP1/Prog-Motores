using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{

    public GameObject[] loottable;
    public void loot()
    {
        int random = Random.Range(1, loottable.Length);
        Instantiate(loottable[random],transform.position + new Vector3(0,2,0),transform.rotation);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        IObject obj = other.GetComponentInParent<IObject>();
        if (other.CompareTag("golpemano")) loot();
        else if (obj != null && obj.inMano) loot();
    }
}
