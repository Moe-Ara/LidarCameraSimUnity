using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples {
public class ConeArrayManager : MonoBehaviour
{


    public List<Vector3> blueConePositions = new List<Vector3>();
    public List<Vector3> yellowConePositions = new List<Vector3>();
    public List<Vector3> startConePositions = new List<Vector3>();
    Vector3 temp;
    public int blueConeCount;
    public int yellowConeCount;
    public int startConeCount;
    public GameObject startConeHolder = null;
    public GameObject yellowConeHolder = null;
    public GameObject blueConeHolder = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        blueConeCount = blueConeHolder.transform.childCount;
        yellowConeCount = yellowConeHolder.transform.childCount;
        startConeCount = startConeHolder.transform.childCount;
        SaveConePositions();
    }

    public void SaveConePositions(){
         if(blueConePositions.Count >= blueConeCount)
            blueConePositions.Clear();
        for(int i = 0; i < blueConeCount; i++){
 
            temp = blueConeHolder.transform.GetChild(i).transform.position;
            blueConePositions.Add(temp);
            

        }
        if(startConePositions.Count >= startConeCount)
            startConePositions.Clear();
        for(int i = 0; i < startConeCount; i++){
           if(startConePositions.Count < startConeCount){
            temp = startConeHolder.transform.GetChild(i).transform.position;
            startConePositions.Add(temp);
            }

        }
        if(yellowConePositions.Count >= yellowConeCount)
            yellowConePositions.Clear();
        for(int i = 0; i < yellowConeCount; i++){
           if(yellowConePositions.Count < yellowConeCount){
            temp = yellowConeHolder.transform.GetChild(i).transform.position;
            yellowConePositions.Add(temp);
            }

        }


    }
}
}