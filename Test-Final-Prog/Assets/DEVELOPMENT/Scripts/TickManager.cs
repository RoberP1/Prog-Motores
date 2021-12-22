using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    public class OnTickEventArgs : EventArgs
    {
        public int tick;
    }

    public static event EventHandler<OnTickEventArgs> OnTick;


    [Range(0, 10000)] public float TicksPerSec;
    private float TickTimerMax = 0.1f;
    public int tick;
    private float tickTimer;

    void Start() => tick = 0;

    void Update()
    {
        TickTimerMax = 1 / TicksPerSec;
        tickTimer += Time.deltaTime;
        if (tickTimer >= TickTimerMax && Time.timeScale !=0)
        {
            tickTimer -= TickTimerMax;
            tick++;
            if (OnTick != null) OnTick(this, new OnTickEventArgs { tick = tick });
        }
    }
}
