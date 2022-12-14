using System;
using UnityEngine;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        public bool closedLoop = true;
        public Transform[] waypoints;

        void Start () {
            PathUpdate();
        }

        public void Update()
        {
            PathUpdate();
        }

        public void PathUpdate()
        {
            
            if (waypoints.Length > 0) {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
                var pathCreator = GetComponent<PathCreator>();
                pathCreator.bezierPath = bezierPath;
                pathCreator.TriggerPathUpdate();
            }
        }
        
    }
}