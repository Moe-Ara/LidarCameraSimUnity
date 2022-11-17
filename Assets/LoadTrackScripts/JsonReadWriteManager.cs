using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace PathCreation.Examples {

public class JsonReadWriteManager : MonoBehaviour
{
    Vector3[] saveFilePos;
    GeneratePathExample example;
    ConeArrayManager cones;
    //SpawnWaypoint spawnScript;
    public GameObject main;
    public GameObject coneManager;
    Transform[] copy;
    public GameObject prefab = null;
    public GameObject nodeHolder = null;
    public int blueConeCount;
    public int yellowConeCount;
    public int startConeCount;
    public GameObject startConeHolder = null;
    public GameObject yellowConeHolder = null;
    public GameObject blueConeHolder = null;
    public GameObject blueCone = null;
    public GameObject yellowCone = null;
    public GameObject startCone = null;
    public GameObject otherTracks = null;
    public GameObject meshHolder;
    public MeshRenderer mr;

   


    
    
    

     
    // Start is called before the first frame update
    void Start()
    {
        example = main.GetComponent<GeneratePathExample>();
        cones = coneManager.GetComponent<ConeArrayManager>();
        //spawnScript = main.GetComponent<SpawnWaypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown("a")){
            SaveToJSON();
        }
        if(Input.GetKeyDown("b")){
            LoadFromJSON();
        }
                
    }


    public void SaveToJSON(){
      //ConeData cd = new ConeData();
      NodeData nd = new NodeData();

     nd.yellowConePositions = new Vector3[cones.yellowConeCount];
     nd.blueConePositions = new Vector3[cones.blueConeCount];
     nd.startConePositions = new Vector3[cones.startConeCount];

     for(int i = 0; i < cones.yellowConeCount; i++){
        nd.yellowConePositions[i] = cones.yellowConePositions[i];
     }
     for(int i = 0; i < cones.startConeCount; i++){
        nd.startConePositions[i] = cones.startConePositions[i];
     }
     for(int i = 0; i < cones.blueConeCount; i++){
        nd.blueConePositions[i] = cones.blueConePositions[i];
     }

      nd.yellowCount = cones.yellowConeCount;
      nd.blueCount = cones.blueConeCount;
      nd.startCount = cones.startConeCount;
     // nd.nodes = example.waypoints;
      nd.nodePositions1 = example.nodePositions;
      nd.arrayLength = example.waypoints.Length;
      nd.closedPath = example.closedLoop;
      string json = JsonUtility.ToJson(nd, true);
      File.WriteAllText(Application.dataPath+"/RoadDataFile.json", json);

      Debug.Log("Path data saved to JSON file!");

    }

    public void LoadFromJSON(){
        ClearScene();


        for(int i = 0;  i<otherTracks.transform.childCount;i++){
            if(otherTracks.transform.GetChild(i).gameObject.activeSelf){
            otherTracks.transform.GetChild(i).gameObject.SetActive(false);        
        }
        }
        string json = File.ReadAllText(Application.dataPath + "/RoadDataFile.json");
        NodeData nd = JsonUtility.FromJson<NodeData>(json);
       // spawnScript.pointCount = nd.arrayLength;
       // example.waypoints = nd.nodes;
        
        //Debug.Log("okay");
        copy = new Transform[nd.arrayLength];
       // Debug.Log("okay");

        AdjustPositions(nd);
        CreateNewPath(nd);
        InsertCones(nd);
        
        
    }


    public void ClearScene(){
        example.waypoints = new Transform[1];

        mr.enabled = false;

        foreach (Transform child in blueConeHolder.transform) {
     GameObject.Destroy(child.gameObject);
 }
        foreach (Transform child in startConeHolder.transform) {
     GameObject.Destroy(child.gameObject);
 }
        foreach (Transform child in yellowConeHolder.transform) {
     GameObject.Destroy(child.gameObject);
 }
    }
    



    public void AdjustPositions(NodeData nd){
        Vector3 offset = nd.nodePositions1[0];

        for(int i = 0; i < nd.arrayLength; i++){
            nd.nodePositions1[i] = nd.nodePositions1[i] - offset;
        }

        for(int i = 0; i < nd.startCount; i++){

            nd.startConePositions[i] = nd.startConePositions[i] - offset;
        }
        for(int i = 0; i < nd.yellowCount; i++){

            nd.yellowConePositions[i] = nd.yellowConePositions[i] - offset;
        }
        for(int i = 0; i < nd.blueCount; i++){

            nd.blueConePositions[i] = nd.blueConePositions[i] - offset;
        }

    }

    public void CreateNewPath(NodeData nd){
        mr.enabled = true;
        //adjust size of the main array
        example.waypoints = copy;
        saveFilePos = nd.nodePositions1;
        
        // give offset to all node positions
        //Vector3 offset = saveFilePos[0];

        

        for(int i = 0; i < nd.arrayLength; i++){
          
         // Debug.Log(saveFilePos[i]);
          example.waypoints[i] = Instantiate(prefab, saveFilePos[i], Quaternion.identity, nodeHolder.transform).GetComponent<Transform>();
          example.closedLoop = nd.closedPath;
          example.waypoints[i].position = saveFilePos[i];
          
        
        }

    }

     public void InsertCones(NodeData nd){

        //give offset to all cone positions
        Vector3 offset = saveFilePos[0];
        var instantiationRot = Quaternion.Euler(-90, 0, 0);



        //instentiate start cones

        startConeCount = nd.startCount;
        saveFilePos = nd.startConePositions;
        for(int i = 0; i < startConeCount; i++){

            Instantiate(startCone, saveFilePos[i], instantiationRot, startConeHolder.transform);
        }

        //instentiate blue cones
        blueConeCount = nd.blueCount;
        saveFilePos = nd.blueConePositions;
         for(int i = 0; i < blueConeCount; i++){

            Instantiate(blueCone, saveFilePos[i], instantiationRot, blueConeHolder.transform);
        }

        //instentiate yellow cones
        yellowConeCount = nd.yellowCount;
        saveFilePos = nd.yellowConePositions;
         for(int i = 0; i < yellowConeCount; i++){

            Instantiate(yellowCone, saveFilePos[i], instantiationRot, yellowConeHolder.transform);
        }
        
    }
}
   
}