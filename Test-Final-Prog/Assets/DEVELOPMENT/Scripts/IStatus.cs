using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class IStatus : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private Manager manager;
    private bool alive;
    [SerializeField] private Slider Vida, Hambre, Sed;

    public float health, hunger, thirst;
    public float healthTick, hungerTick, thirstTick;
    public float healthMax, hungerMax, thirstMax;
    public float hungerTickWalk, thirstTickWalk;
    public float hungerTickSprint, thirstTickSprint;
    public float healthStatusTick,hungerStatusTick, thirstStatusTick = 0;

    void Start()
    {
        alive = true;
        manager = FindObjectOfType<Manager>();
        _input = GetComponent<StarterAssetsInputs>();
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
        if (thirst > thirstMax) thirst = thirstMax;
        if (thirst > 0) thirst -= thirstMax / thirstTick;
    }
    private void ReducirHambre(float hungerTick)
    {
        if (hunger > hungerMax) hunger = hungerMax;
        if (hunger > 0) hunger -= hungerMax / hungerTick;
    }
    private void AumentarVida(float healthTick)
    {
        if (health <= 0 && alive)
        {
            alive = false;
            manager.Perder();
        }
        if (health < healthMax && hunger > 70 && thirst > 70) health += healthMax / healthTick;
        if (hunger == 0 || thirst == 0) health -= healthMax / (healthTick * 3);
    }
    private void UpdateUI()
    {
        Vida.value = health;
        Hambre.value = hunger;
        Sed.value = thirst;
    }
    public void RecibirDamage(float d) => health -= d;
    public void Curar(float h) => health += h;
    public void Comer(float c) => hunger += c;
    public void Beber(float b) => thirst += b;

    public void estado(float v,float h,float s) 
    {
        healthStatusTick += v;
        hungerStatusTick +=h;
        thirstStatusTick +=s;
    }
}
