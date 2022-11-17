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
using PathCreation.Examples;

/// <summary>
/// This Class is Responsible for changing the Track
/// </summary>
public class TrackManager : MonoBehaviour
{
    public GameObject jsonTrack;
    public List<GameObject> Tracks; //List of Available tracks
    public List<GameObject> TrackImgs;
    public GameObject CurrentTrack, currentImg; //Current Track
    // public AutoPathFollower autoPathFollower; // Autopath follower of the car

    /// <summary>
    /// Method to change tracks
    /// </summary>
    /// <param name="track">track that we want to switch to</param>
    public void SetTrack(string track)
    {
        CurrentTrack.SetActive(false);
        //find the current track
        CurrentTrack = Tracks.SingleOrDefault(x =>
        {
            if (x != null)
                return x.name == track;
            return false;
        });
       
        //set track to active
        try
        {
            
            jsonTrack.GetComponent<JsonReadWriteManager>().ClearScene();
            CurrentTrack.SetActive(true);
        }
        //if a deleted track is chosen
        catch (NullReferenceException e)
        {
            CurrentTrack = Tracks[0];
            CurrentTrack.SetActive(true);
            Console.Out.WriteLine(e.Message);
        }


        //for auto path follower      
        // //instruct the car to follow the newly set track
        // autoPathFollower.pathCreator = CurrentTrack.GetComponent<PathCreator>();
    }
}