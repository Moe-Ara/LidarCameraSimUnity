using System.Collections;
using System.Collections.Generic;
using System.Text;
using Car.Camera;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Communication;
using UnityEditor;

public class PublishingAndSubscribingTest
{
    //~Camera
    private Publisher CamPub;
    private Subscriber CamSub;
    
    //~ Reusables
    private GameObject car;
    private bool data;
    private byte[] msg;

    
    [SetUp]
    public void Setup()
    {
        {
            //~ CAMERA DATA TEST
            car = GameObject.Instantiate(Resources.Load<GameObject>("Car"));
            var _carCamCont =car.GetComponent<CarCameraController>();
            data = false;
            CamPub = new Publisher(42000);
            _carCamCont.OnNewImage = msg => CamPub.Publish(msg);
            msg = Encoding.UTF8.GetBytes("Hello I am Publisher");
            CamSub = new Subscriber(42000, onNewData);
        }
        
    }
    
    [UnityTest]
    public IEnumerator CameraData()
    {  
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        CamPub.Publish(msg);
        yield return new WaitForSeconds(0.3f);
        Assert.IsTrue(data);
    }

    private void onNewData(byte[] d)
    {
        if(d!=null)
            data = true;
    }
}
