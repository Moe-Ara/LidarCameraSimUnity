using PathCreation;
using PathCreation.Examples;
using UnityEngine;

namespace Track
{
    [ExecuteInEditMode]
    public class ConePathPlacer : PathSceneTool
    {
        public GameObject leftConeprefab;
        public GameObject rightConeprefab;
        public GameObject leftStartingConeprefab;
        public GameObject rightStartingConeprefab;
        public GameObject holder;
        public float spacing = 3;
        public float trackwidth = 3;

        private const float minSpacing = .1f;

        private Vector3 riseCones = new(0, 0, 0);

        //Place Cones along track
        private void Generate()
        {
            if (pathCreator != null && leftConeprefab != null && rightConeprefab != null &&
                leftStartingConeprefab != null && rightStartingConeprefab != null && holder != null)
            {
                DestroyObjects();
                var path = pathCreator.path;
                spacing = Mathf.Max(minSpacing, spacing);
                float dst = 0;


                while (dst < path.length)
                {
                    var point = path.GetPointAtDistance(dst);

                    var nextpoint = path.GetPointAtDistance(dst + 1);
                    var directionVector = nextpoint - point;

                    //Calculate the directional vector for the cones
                    var widthDistance = Vector3.Cross(directionVector, new Vector3(0, 1, 0)) * trackwidth / 2;

                    var instantiationRot = Quaternion.Euler(-90, 0, 0);


                    //Place Starting Cones
                    if (dst == 0)
                    {
                        Instantiate(leftStartingConeprefab, point - widthDistance, instantiationRot,
                            holder.transform);
                        Instantiate(rightStartingConeprefab, point + widthDistance, instantiationRot,
                            holder.transform);
                    }
                    else
                    {
                        //place Cones left and right of the track
                        Instantiate(leftConeprefab, point + widthDistance + riseCones, instantiationRot,
                            holder.transform);
                        Instantiate(rightConeprefab, point - widthDistance + riseCones, instantiationRot,
                            holder.transform);
                    }

                    dst += spacing;
                }
            }
        }

        private void DestroyObjects()
        {
            var numChildren = holder.transform.childCount;
            for (var i = numChildren - 1; i >= 0; i--) DestroyImmediate(holder.transform.GetChild(i).gameObject, false);
        }

        protected override void PathUpdated()
        {
            if (pathCreator != null) Generate();
        }
    }
}