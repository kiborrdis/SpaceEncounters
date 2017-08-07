using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceEncounter
{
    public class TargetInfo
    {
        private Vector3 location;
        private Vector3 velocity;
        private Vector3 acceleration;

        public TargetInfo(Vector3 location) : this(location, Vector3.zero, Vector3.zero) { }

        public TargetInfo(Vector3 location, Vector3 velocity) : this(location, velocity, Vector3.zero) { }

        public TargetInfo(Vector3 location, Vector3 velocity, Vector3 acceleration)
        {
            this.location = location;
            this.velocity = velocity;
            this.acceleration = acceleration;
        }

        public Vector3 Location
        {
            get
            {
                return location;
            }
        }
        
        public Vector3 Velocity
        {
            get
            {
                return velocity;
            }
        }

        public Vector3 Acceleration
        {
            get
            {
                return acceleration;
            }
        }
    }
}
