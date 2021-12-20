using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    private bool day = true;
    public int dayDuration;
    public Material daymat;
    public Material nightmat;
    
    void Start()
    {
        TickManager.OnTick += DayTick;
    }

    
    private void DayTick(object sender, TickManager.OnTickEventArgs e)
    {
        transform.rotation = Quaternion.Euler(e.tick / (dayDuration / 180), 0, 0);
        if (e.tick % dayDuration == 0) ChangeSkybox(); 
    }

    private void ChangeSkybox()
    {
        day = !day;
        RenderSettings.skybox = day ? daymat : nightmat;
    }
}
