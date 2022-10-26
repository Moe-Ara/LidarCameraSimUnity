using System.Collections;
using System.Collections.Generic;
using System.Text;
using Car.Camera;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Communication;
using UnityEditor;

public class NewTestScript
{
    private GameObject car;
    private Camera cam;
    private bool camData;
    private Publisher CamPub;
    Subscriber CamSub;
    private byte[] msg;
    private CarCameraController _carCamCont;
    
    [SetUp]
    public void Setup()
    {
        {
            //~ CAMERA DATA TEST
            car = GameObject.Instantiate(Resources.Load<GameObject>("Car"));
            _carCamCont =car.GetComponent<CarCameraController>();
            camData = false;
            CamPub = new Publisher(42000);
            _carCamCont.OnNewImage = msg => CamPub.Publish(msg);
            msg = Encoding.UTF8.GetBytes("Hello I am Publisher");
            CamSub = new Subscriber(42000, onNewData);
        }
        
    }
    
    [UnityTest]
    public IEnumerator IsThereCameraData()
    {  
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        
        CamPub.Publish(msg);
        yield return new WaitForSeconds(0.3f);
        // Assert.IsTrue(camData);
        Assert.IsTrue(camData);

    }

    private void onNewData(byte[] data)
    {
        if(data!=null)
            camData = true;
    }
}
