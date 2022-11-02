using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using PathCreation;
using Track;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;
using PathCreation.Utility;

/// <summary>
/// This Class is Responsible for changing the Track
/// </summary>
public class TrackManager : MonoBehaviour
{
    public List<GameObject> Tracks; //List of Available tracks
    public List<GameObject> TrackImgs; 
    public GameObject CurrentTrack,currentImg; //Current Track
    public AutoPathFollower autoPathFollower; // Autopath follower of the car
    
    
    public void SetTrack(string track)
    {
      
        // ResetCar();

        //hide all tracks
        //(note: maybe find a better way so we don't have to iterate over all tracks each time we set one)
        foreach (var t in Tracks) t.SetActive(false);

        //find the current track
        CurrentTrack = Tracks.Where(x => x.name == track).SingleOrDefault();
        for (int i = 0; i < TrackImgs.Count ; i++)
        {
            if (TrackImgs[i].name == track)
            {
                TrackImgs[i].SetActive((true ));
            }
            else
            {
                TrackImgs[i].SetActive((false ));
            }
        }
     

        
        //set track to active
        CurrentTrack.SetActive(true);
        
      
            //instruct the car to follow the newly set track
            autoPathFollower.pathCreator = CurrentTrack.GetComponent<PathCreator>();
            
    }

    private void ResetCar()
    {
        //TODO: IMPLEMENT THIS (OR DELETE METHOD)
//        autoPathFollower.speed = 0;
        

    }
}