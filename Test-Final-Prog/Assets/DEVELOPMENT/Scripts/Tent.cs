using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    private TickManager tickManager;
    private DayNight DayNight;
    private bool esta = false;
    void Start()
    {
        tickManager = FindObjectOfType<TickManager>();
        DayNight = FindObjectOfType<DayNight>();
    }
    private void Update()
    {
        if (!DayNight.day && Input.GetKeyDown(KeyCode.E) && esta)
            tickManager.tick += 15000;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) esta = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) esta = false;
    }
}
