using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private IStatus player;
    private bool CanMakeDamage;
    void Start()
    {
        player = FindObjectOfType<IStatus>();
        CanMakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (CanMakeDamage)
            {
                player.Curar(-25);
                StartCoroutine(DelayDaño(1.5f));
            }
        }
    }
    public IEnumerator DelayDaño(float delay)
    {
        CanMakeDamage = false;
        yield return new WaitForSeconds(delay);
        CanMakeDamage = true;
    }
}
