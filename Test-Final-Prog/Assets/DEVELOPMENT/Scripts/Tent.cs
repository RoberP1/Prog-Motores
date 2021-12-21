using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    private TickManager tickManager;
    private DayNight DayNight;
    void Start()
    {
        tickManager = FindObjectOfType<TickManager>();
        DayNight = FindObjectOfType<DayNight>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (!DayNight.day && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            tickManager.tick += 15000;
        }
    }
}
