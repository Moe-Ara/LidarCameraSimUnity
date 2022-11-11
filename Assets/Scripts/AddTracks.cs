using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Helper class to add all tracks to the list of tracks when the simulator starts
/// </summary>
public class AddTracks : MonoBehaviour
{
    public TrackManager trackManager;
    public MainMenuManger MainMenuManger;
    // Start is called before the first frame update
    private void Start()
    {
        addTracks();
        //set the current track to the first track we added
        trackManager.CurrentTrack = trackManager.Tracks[0];
        
    }

    // Update is called once per frame
    private void Update()
    {
    
        
        //check if a new track is added
        if (gameObject.transform.childCount!=trackManager.Tracks.Count)
        {
            //safe but slow
            addTracks();
            //not safe but fast
            // updateTracks();
        }
    }

    /// <summary>
    /// Updates the list of available tracks
    /// </summary>
    private void addTracks()
    {
        while (true)
        {
            //adding tracks
            if (gameObject.transform.childCount > trackManager.Tracks.Count)
            {
                //get all children of this gameobject
                foreach (Transform child in transform)
                {
                    //if the track is already added return 
                    if (trackManager.Tracks.Contains(child.gameObject)) continue;
                    trackManager.Tracks.Add(child.gameObject);
                    MainMenuManger.AddNewTrack(child.gameObject.name);
                    
                    //setting new added tracks to 'not active'
                    if (trackManager.Tracks[0] != child.gameObject) child.gameObject.SetActive(false);
                }
            }
            //deleting tracks
            else if (gameObject.transform.childCount < trackManager.Tracks.Count)
            {
                //TODO: Find a better way to doing this
                trackManager.Tracks.Clear();
                MainMenuManger.DeleteTracks();
                continue;
            }

            break;
        }
    }

    private void updateTracks()
    {
        while (true)
        {
            var differenceInSizes = gameObject.transform.childCount - trackManager.Tracks.Count;
            switch (differenceInSizes)
            {
                //adding new tracks
                case > 0:
                    trackManager.Tracks.Add(transform.GetChild(trackManager.Tracks.Count).gameObject);
                    MainMenuManger.AddNewTrack(transform.GetChild(trackManager.Tracks.Count).gameObject.name);
                    //setting new added tracks to 'not active'
                    transform.GetChild(trackManager.Tracks.Count - 1).gameObject.SetActive(false);
                    continue;
                //deleting tracks
                case < 0:
                    //switch to first track if the deleted track was active
                    if (trackManager.CurrentTrack == null) trackManager.SetTrack(trackManager.Tracks[0].name);
                    //slow but safe
                    addTracks();
                    continue;
            }

            break;
        }
    }
}
