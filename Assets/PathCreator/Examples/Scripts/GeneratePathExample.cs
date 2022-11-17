using UnityEngine;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        
      //  public GameObject path;
        public bool closedLoop = false;
        [SerializeField] public Transform[] waypoints = new Transform[2];
        public Vector3[] nodePositions;

        

        void Start(){
      //  b = path.GetComponent<SpawnWaypoint>();

        }
        void Update(){
                if(Input.GetKeyDown("3")){
                    closedLoop = !closedLoop;
                }
            
           //  b = path.GetComponent<SpawnWaypoint>();
           //if(Input.GetKeyDown("x"))  {
              if (waypoints.Length > 1) {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xz);
                GetComponent<PathCreator> ().bezierPath = bezierPath;

                nodePositions = new Vector3[waypoints.Length];
            for(int i = 0; i < waypoints.Length; i++){

            nodePositions[i] = new Vector3(waypoints[i].position.x, waypoints[i].position.y ,waypoints[i].position.z);
          }
            
        }
        }
    }
}