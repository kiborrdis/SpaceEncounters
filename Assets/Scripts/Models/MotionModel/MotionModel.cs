using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceEncounter
{
    public class MotionModel : Model
    {

        public enum Rotation { stale, left, right };
        public enum Acceleration { stale, forward, backward, left, right, forwardLeft, forwardRight, backwardLeft, backwardRight };

        public float maxSpeed = 5.0f;
        public float cruisingSpeed = 5.0f;
        public float rotationSpeed = 10.0f;
        public float accel = 0.1f;

        private float rotationRadSpeed;
        private Vector3 desiredHeading;
        private Vector3 desiredVelocity;
        private Vector3 velocity;

        Rotation currentRotation = Rotation.stale;
        Acceleration currentAcceleration = Acceleration.stale;

        public Vector3 Velocity
        {
            set
            {
                velocity = value;
            }
            get
            {
                return velocity;
            }
        }


        public float RotationSpeed
        {
            set
            {
                rotationRadSpeed = value;
            }
            get
            {
                return rotationRadSpeed;
            }
        }

        public Vector3 DesiredVelocity
        {
            set
            {
                desiredVelocity = value;
            }
            get
            {
                return desiredVelocity;
            }
        }

        public Vector3 DesiredHeading
        {
            set
            {
                desiredHeading = value;
            }
            get
            {
                return desiredHeading;
            }
        }

        public Rotation rotation
        {
            set
            {

                currentRotation = value;
            }
            get
            {
                return currentRotation;
            }
        }

        public Acceleration acceleration
        {
            set
            {
                currentAcceleration = value;
            }
            get
            {
                return currentAcceleration;
            }
        }

        public void applyRotationByVectors(Vector3 currentDirection, Vector3 desiredDirection)
        {
            Vector3 cross = Vector3.Cross(currentDirection, desiredDirection);

            if (cross.y < 0)
            {
                rotation = MotionModel.Rotation.left;
            }
            else if (cross.y > 0)
            {
                rotation = MotionModel.Rotation.right;
            }
            else
            {
                rotation = MotionModel.Rotation.stale;
            }
        }

        public void applyModelAccordingDelta(Vector3 localDirection)
        {
            localDirection.x = Mathf.Round(localDirection.x * 10) / 10;
            localDirection.y = Mathf.Round(localDirection.y * 10) / 10;
            localDirection.z = Mathf.Round(localDirection.z * 10) / 10;

            if (localDirection.x > 0 && localDirection.z > 0)
            {
                acceleration = (MotionModel.Acceleration.forwardRight);
            }
            else if (localDirection.x > 0 && localDirection.z < 0)
            {
                acceleration = (MotionModel.Acceleration.backwardRight);
            }
            else if (localDirection.x < 0 && localDirection.z > 0)
            {
                acceleration = (MotionModel.Acceleration.forwardLeft);
            }
            else if (localDirection.x < 0 && localDirection.z < 0)
            {
                acceleration = (MotionModel.Acceleration.backwardLeft);
            }
            else if (localDirection.x == 0 && localDirection.z != 0)
            {
                acceleration = (localDirection.z > 0 ? MotionModel.Acceleration.forward : MotionModel.Acceleration.backward);
            }
            else if (localDirection.z == 0 && localDirection.x != 0)
            {
                acceleration = (localDirection.x > 0 ? MotionModel.Acceleration.right : MotionModel.Acceleration.left);
            }
            else
            {
                acceleration = MotionModel.Acceleration.stale;
            }
        }
    }
}