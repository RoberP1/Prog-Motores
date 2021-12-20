using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class IStatus : MonoBehaviour
{
    private TickManager ticksManager;
    private StarterAssetsInputs _input;
    private Manager manager;
    [SerializeField] private Slider Vida, Hambre, Sed;

    public float health, hunger, thirst;
    public float healthTick, hungerTick, thirstTick;
    public float healthMax, hungerMax, thirstMax;
    public float hungerTickWalk, thirstTickWalk;
    public float hungerTickSprint, thirstTickSprint;
    public float healthStatusTick,hungerStatusTick, thirstStatusTick = 0;
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        _input = GetComponent<StarterAssetsInputs>();
        ticksManager = FindObjectOfType<TickManager>();
        ActualizarTick();
    }
    private void ActualizarTick()
    {
        TickManager.OnTick += TimeTickSystem_OnTick;
    }
    private void TimeTickSystem_OnTick(object sender, TickManager.OnTickEventArgs e)
    {
        hungerTick = _input.sprint ? hungerTickSprint : hungerTickWalk;
        thirstTick = _input.sprint ? thirstTickSprint : thirstTickWalk;
        ReducirHambre(hungerTick + hungerStatusTick);
        ReducirSed(thirstTick + thirstStatusTick);
        AumentarVida(healthTick + healthStatusTick);
        UpdateUI();
    }
    private void ReducirSed(float thirstTick)
    {
        if (thirst > 0) thirst -= thirstMax / thirstTick;
    }
    private void ReducirHambre(float hungerTick)
    {
        if (hunger > 0) hunger -= hungerMax / hungerTick;
    }
    private void AumentarVida(float healthTick)
    {
        if (health <= 0) manager.Perder();
        if (health < healthMax && hunger > 30 && thirst > 30) health += healthMax / healthTick;
        if (health >= healthMax) health = healthMax;
    }
    private void UpdateUI()
    {
        Vida.value = health;
        Hambre.value = hunger;
        Sed.value = thirst;
    }
    public void RecibirDamage(float d)
    {
        health -= d;
    }
    public void Curar(float h)
    {
        health += h;
    }
    public void Comer(float c)
    {
        hunger += c;
        if (hunger > hungerMax) hunger = hungerMax;
    }
    public void Beber(float b)
    {
        thirst += b;
        if (thirst > thirstMax) thirst = thirstMax;
    }
    public void estado(float v,float h,float s) 
    {
        healthStatusTick += v;
        hungerStatusTick +=h;
        thirstStatusTick +=s;
    }
}
