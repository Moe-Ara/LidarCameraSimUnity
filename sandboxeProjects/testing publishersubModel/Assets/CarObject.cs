using System;
using System.Collections;
using System.Collections.Generic;
using com;
using UnityEngine;
using UnityEngine.Serialization;

public class CarObject : MonoBehaviour
{
public float speed;
public float yawRate;
private Rigidbody _rigidbody;

public void Start()
{
    _rigidbody = GetComponent<Rigidbody>();
}

public void MoveCar(msgRecv msgRecv)
{
    speed = msgRecv.speed;
    yawRate = msgRecv.yaw;

}

private void Update()
{
    Vector3 temp = new Vector3(0, 0, 1);
    temp = temp.normalized * (speed * Time.deltaTime);
    transform.rotation=Quaternion.Euler(0f,yawRate,0f);
    transform.position += transform.forward * (Time.deltaTime * speed);
    
}
}
