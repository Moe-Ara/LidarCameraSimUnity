using PathCreation;
using UnityEngine;

namespace Track
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class AutoPathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        private float _distanceTravelled;
        

        private void Start()
        {
            if (pathCreator != null)
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
        }

        private void FixedUpdate()
        {
            
            if (pathCreator == null) return;
            //!!!!REMOVE HERE IF YOU REMOVE CART FEATURE
            if (transform.name=="cart")
            {
                //these are for the cart MIGHT WANT TO REMOVE LATER        
                _distanceTravelled += speed * Time.fixedDeltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction) +
                                     new Vector3(0, 1.68f, 0);
                transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
                transform.Rotate(180,0,90);
                
                
            }
            else
            {
                //these are for the car
                _distanceTravelled += speed * Time.fixedDeltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
                transform.Rotate(0, 0, 90);
            }
            

            
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        private void OnPathChanged()
        {
            _distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}