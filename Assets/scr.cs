using System.Collections;
using System.Collections.Generic;
using Car.gss;
using Car.imu;
using Car.Lidar;
using UnityEngine;

public class scr : MonoBehaviour
{
    [SerializeField] private GssController _gssController; //speed sensor

    [SerializeField] private LidarController _lidarController;

    [SerializeField] private Imu_Controller _imuController;

    [SerializeField] private GameObject _car;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
