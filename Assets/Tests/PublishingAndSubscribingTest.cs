using System.Collections;
using System.Collections.Generic;
using System.Text;
using Car.Camera;
using Car.Lidar;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Communication;
using UnityEditor;


public class PublishingAndSubscribingTest
{
    
    /// <summary>
    /// Class for test cases
    /// </summary>
    
    //~Camera
    private Publisher CamPub;
    private Subscriber CamSub;

    //~ Reusables
    private GameObject car;
    private bool Camdata;
    private bool LidData;
    private byte[] msg;

    /// <summary>
    /// Set up
    /// </summary>
    [SetUp]
    public void Setup()
    {
        car = GameObject.Instantiate(Resources.Load<GameObject>("Car"));
        {
            //~ CAMERA DATA TEST
            var _carCamCont =car.GetComponent<CarCameraController>();
            Camdata = false;

            CamPub = new Publisher(42000);
            _carCamCont.OnNewImage = msg => CamPub.Publish(msg);
            msg = Encoding.UTF8.GetBytes("Hello I am Publisher");
            CamSub = new Subscriber(42000, onNewCamData);
        }
    }
    /// <summary>
    /// testing if the camera publisher and subscriber are working 
    /// </summary>
    [UnityTest]
    public IEnumerator CameraData()
    {  
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        CamPub.Publish(msg);
        yield return new WaitForSeconds(0.3f);
        Assert.IsTrue(Camdata);
    }
    

    private void onNewCamData(byte[] d)
    {
        if(d!=null)
            Camdata = true;
    }

}
