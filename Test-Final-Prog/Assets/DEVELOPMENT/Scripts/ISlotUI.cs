using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ISlotUI : MonoBehaviour
{
    public Text itemName;
    public Text itemDescription;
    public Text itemQuantity;
    public Button slot;

    public RawImage icon;
    public Slider itemDamage;


    public Transform defaultImagePos;
    public Vector3 offset;
    public Camera cam;

    public bool siguiendoCam;
    public bool Isfollowing = false;
}

