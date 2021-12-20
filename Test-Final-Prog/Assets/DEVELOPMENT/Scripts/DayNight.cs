using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    private bool day = true;
    public GameObject yo;
    public int dayDuration;
    public Material daymat;
    public Material nightmat;

    
    void Awake()
    {
        yo = FindObjectOfType<DayNight>().gameObject;
        TickManager.OnTick += DayTick;
    }

    
    private void DayTick(object sender, TickManager.OnTickEventArgs e)
    {
        if(yo == null) yo = FindObjectOfType<DayNight>().gameObject; //si esto no esta al reiniciar la escena se pierde
        yo.transform.rotation = Quaternion.Euler(e.tick / (dayDuration / 180), 0, 0);
        if (e.tick % dayDuration == 0) ChangeSkybox(); 
    }

    private void ChangeSkybox()
    {
        day = !day;
        RenderSettings.skybox = day ? daymat : nightmat;
    }
}
