using System.Collections;
using System.Collections.Generic;
using UnityEngine;






    [System.Serializable]
public class NodeData
{

  public Vector3[] nodePositions1;
  public int arrayLength;
  public bool closedPath;


  public Vector3[] blueConePositions;
  public Vector3[] startConePositions;
  public Vector3[] yellowConePositions;
  
  
  public int yellowCount;
  public int blueCount;
  public int startCount;

}
