using System.Collections;
using System.Collections.Generic;
using System.Text;
using com;
using UnityEngine;

public class ComInterface : MonoBehaviour
{
    public CarObject _Car;
    private Subscriber carSub;
    private const int PubPort= 10101;
    // Start is called before the first frame update
    void Start()
    {
        carSub = new Subscriber(PubPort, handleData);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void handleData(byte[] msg)
    {
        // Debug.Log("CalledHandle \n");
        // Debug.Log("msg is being read like: " + Encoding.UTF8.GetString(msg));
        var Result = JsonUtility.FromJson<msgRecv>(Encoding.UTF8.GetString(msg));
        _Car.MoveCar(Result);
        
    }
}