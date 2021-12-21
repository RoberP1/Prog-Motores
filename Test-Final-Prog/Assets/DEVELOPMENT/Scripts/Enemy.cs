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
    public bool CanMakeDamage;
    public bool rugir;

    [SerializeField] private Slider Vida;
    [SerializeField] private float distancia;

    public AudioSource reproductor;
    public AudioClip sonido;
    public AudioClip rugido;

    void Start()
    {
        alive = true;
        Vida.value = health;
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<IStatus>();
        target = player.transform;
        CanMakeDamage = true;
        rugir = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (alive && Vector3.Distance(transform.position, target.position) < distancia)
        { 
            agent.SetDestination(target.position);
            if (rugir) StartCoroutine(Rugir(5));


        }
        else agent.SetDestination(transform.position);
    }
    public void Damage(float damage)
    {
        health -= damage;
        Vida.value = health;
        reproductor.clip = sonido;
        reproductor.Play();
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
    public IEnumerator Rugir(float delay)
    {
        rugir = false;
        reproductor.clip = rugido;
        reproductor.Play();
        yield return new WaitForSeconds(delay);
        rugir = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        IObject obj = other.GetComponentInParent<IObject>();
        if (other.CompareTag("golpemano"))
        {
            Damage(20);
        } else if (obj != null && obj.inMano)
        {
            Damage(obj.damage);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && CanMakeDamage && alive)
        {
            player.Curar(-20);
            StartCoroutine(DelayDaño(1.5f));
        }
    }

}
