using System.Collections;
using System.Collections.Generic;
using Car.Camera;
using UnityEngine;
using UnityEngine.UI;

public class getRenderTexture : MonoBehaviour
{
    /// <summary>
    /// This Script is just to get the camera feed from the car camera
    /// </summary>
    
    public Camera CarCamera;
    
    // Start is called before the first frame update
    private void Start()
    {
        var texture = CarCamera.targetTexture;
        gameObject.GetComponent<RawImage>().texture = texture;
    }
}
