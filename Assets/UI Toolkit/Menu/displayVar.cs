using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class displayVar : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI mass; 
    [SerializeField] public TextMeshProUGUI speed;
    [SerializeField] public TextMeshProUGUI GSS;
    [SerializeField] public TextMeshProUGUI IMU;
    private void Start()
    {
        mass.SetText($"mass  500 kg");
        speed.SetText($"speed 50 km/h");
        GSS.SetText($"GSS   x:0  y:0  z:0");
        IMU.SetText($"IMU    x:0  y:0  z:0");
    }
}
