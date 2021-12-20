using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    private float health = 100f;
    private NavMeshAgent agent;
    private Transform target;
    private IStatus player;
    private bool alive = true;
    private bool CanMakeDamage;
    [SerializeField] private Slider Vida;
    [SerializeField] private float distancia;

    void Start()
    {
        alive = true;
        Vida.value = health;
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<IStatus>();
        target = player.transform;
        CanMakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && Vector3.Distance(transform.position, target.position) < distancia)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
            

        if (Vector3.Distance(transform.position, target.position) <= 2.1f) //esto se hace mas eficiente con un trigger
        {
            if (CanMakeDamage && alive)
            {
                player.Curar(-20);
                StartCoroutine(DelayDaño(1.5f));
            }
        }
    }
    public void Damage(float damage)
    {
        health -= damage;
        Vida.value = health;

        if (alive && health <= 0)
        {
            alive = false;
            Destroy(gameObject);
        }

    }
    public IEnumerator DelayDaño(float delay)
    {
        CanMakeDamage = false;
        yield return new WaitForSeconds(delay);
        CanMakeDamage = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        //print("trigger");
        //print(other.gameObject.name);
        if (other.gameObject.name == "AttackArea")//cambiar a trytogetcomponent<Iobject>(out Iobject arma) y si arma.isarma
        {
            print("pego");
            Damage(50);
        }
    }
}
