using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTracks : MonoBehaviour
{
    public TrackManager trackManager;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            trackManager.Tracks.Add(child.gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
