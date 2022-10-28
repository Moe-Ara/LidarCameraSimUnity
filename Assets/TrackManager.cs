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
    /*These Are Objects from the original project
    // public GameObject TrackEndurance;
    // public GameObject TrackAcceleration;
    // public GameObject TrackSkidpad;*/
    
    
    
    public List<GameObject> Tracks; //List of Available tracks
    public List<GameObject> TrackImgs; 
    public GameObject CurrentTrack,currentImg; //Current Track
    public AutoPathFollower autoPathFollower; // Autopath follower of the car
    public AutoPathFollower autoPathFollowerCart;   //!!!!REMOVE HERE IF YOU REMOVE CART FEATURE
    public bool isCartActive;
    
    
    public void SetTrack(string track)
    {
      
        // ResetCar();
        /*Old Code
                 // hide all tracks
        // TrackEndurance.GetComponent<ConePathPlacer>().holder.SetActive(false);
        // TrackAcceleration.GetComponent<ConePathPlacer>().holder.SetActive(false);
        // TrackSkidpad.GetComponent<ConePathPlacer>().holder.SetActive(false);
         */
        
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
        
        //!!!!REMOVE HERE IF YOU REMOVE CART FEATURE
        if (isCartActive ==true)
        {
            autoPathFollowerCart.pathCreator = CurrentTrack.GetComponent<PathCreator>();
        }
        else
        {
            //instruct the car to follow the newly set track
            autoPathFollower.pathCreator = CurrentTrack.GetComponent<PathCreator>();
        }

        
        /* Old Code       
        // switch (track)
        // {
        //     case "endurance":
        //         TrackEndurance.GetComponent<ConePathPlacer>().holder.SetActive(true);
        //         // Set the path for the Car
        //         autoPathFollower.pathCreator = TrackEndurance.GetComponent<PathCreator>();
        //
        //         break;
        //     case "acceleration":
        //         TrackAcceleration.GetComponent<ConePathPlacer>().holder.SetActive(true);
        //         autoPathFollower.pathCreator = TrackAcceleration.GetComponent<PathCreator>();
        //         break;
        //     case "skidpad":
        //         TrackSkidpad.GetComponent<ConePathPlacer>().holder.SetActive(true);
        //         autoPathFollower.pathCreator = TrackSkidpad.GetComponent<PathCreator>();
        //         break;
        //     default:
        //         break;
        //         
        // }*/
    }

    private void ResetCar()
    {
        //TODO: IMPLEMENT THIS (OR DELETE METHOD)
//        autoPathFollower.speed = 0;
        

    }
}