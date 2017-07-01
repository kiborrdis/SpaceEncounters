using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrajectorySolver : ScriptableObject {
    public struct Trajectory
    {
        public Trajectory(Vector3 velocity, Vector3 endPoint, float travelTime)
        {
            this.velocity = velocity;
            this.endPoint = endPoint;
            this.travelTime = travelTime;
        }

        public Vector3 velocity;
        public Vector3 endPoint;
        public float travelTime;
    }

    public abstract Trajectory calculateTrajectoryFromTo(Vector3 position, Vector3 velocity, Vector3 targetPosition, Vector3 targetVelocity, float maxSpeed);
}
