using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private IStatus player;
    private bool CanMakeDamage;
    public float damage;
    void Start()
    {
        player = FindObjectOfType<IStatus>();
        CanMakeDamage = true;
    }
    public IEnumerator DelayDa�o(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.estado(damage, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) player.estado(-damage, 0, 0);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) StartCoroutine(DelayDa�o(3));
    }
}
