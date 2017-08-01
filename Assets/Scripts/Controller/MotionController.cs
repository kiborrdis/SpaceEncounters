using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class MotionController : MotionBasedController
    {
        Rigidbody objectRigidbody;

        bool rotateTowardsHeading(Vector3 desiredHeading)
        {
            Vector3 currentHeading = transform.rotation * Vector3.forward;
            currentHeading.Normalize();

            if (Vector3.Angle(currentHeading, desiredHeading) > 0.1f)
            {
                Model.applyRotationByVectors(currentHeading, desiredHeading);
                Vector3 newHeading = Vector3.RotateTowards(currentHeading, desiredHeading, Model.RotationSpeed * Time.deltaTime, 10);

                if (newHeading.magnitude != 0)
                {
                    objectRigidbody.MoveRotation(Quaternion.LookRotation(newHeading));
                }
            }
            else
            {
                Model.rotation = MotionModel.Rotation.stale;
            }


            return Vector3.Angle(transform.rotation * Vector3.forward, desiredHeading) < 30.0f;
        }

        void changeVelocity(Vector3 desiredVelocity, Vector3 desiredHeading)
        {
            if ((!rotateTowardsHeading(desiredHeading) || Vector3.Distance(desiredVelocity, objectRigidbody.velocity) < 0.5f) && desiredVelocity.magnitude != 0)
            {
                Model.applyModelAccordingDelta(Vector3.zero);
                return;
            }

            //Debug.Log("Accelerate... " + Vector3.Distance(desiredVelocity, engine.velocity));
            float frameDeltaVelocity = Model.accel * Time.deltaTime;

            Vector3 delta = desiredVelocity - objectRigidbody.velocity;
            float deltaVelocity = delta.magnitude;

            objectRigidbody.AddForce(delta.normalized * (deltaVelocity > frameDeltaVelocity ? frameDeltaVelocity : deltaVelocity), ForceMode.VelocityChange);

            frameDeltaVelocity = deltaVelocity > frameDeltaVelocity ? 0 : (frameDeltaVelocity - deltaVelocity);

            Model.applyModelAccordingDelta(transform.InverseTransformDirection(delta.normalized));
        }

        protected override void Start()
        {
            base.Start();

            objectRigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            changeVelocity(Model.DesiredVelocity, Model.DesiredHeading);
        }
    }
}