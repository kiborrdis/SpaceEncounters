using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceEncounter
{
    public class PlayerController : MotionBasedController
    {

        Rigidbody playerRigidbody;

        public TestEditorClass foo;

        public bool decelerateWithoutControls = false;

        private Vector3 angleVelocity;

        protected override void Start()
        {
            base.Start();

            playerRigidbody = this.GetComponent<Rigidbody>();

            angleVelocity = new Vector3(0, Model.rotationSpeed, 0);

            if (!playerRigidbody)
            {
                Debug.Log("Player's rigidbody haven't found");
            }
        }

        void Update()
        {
            applyHeadingChanges();
            applyVelocityChanges();

            Model.Velocity = playerRigidbody.velocity;

            GameModel.Instance.Velocity = playerRigidbody.velocity.magnitude;
        }

        void applyModelData(MotionModel.Acceleration accel)
        {
            if (Model != null && Model.acceleration != accel)
            {
                Model.acceleration = accel;
            }
        }

        void applyModelData(MotionModel.Rotation rotation)
        {
            if (Model != null && Model.rotation != rotation)
            {
                Model.rotation = rotation;
            }
        }

        void applyModelAccordingDelta(Vector3 delta)
        {
            Vector3 localDirection = transform.InverseTransformDirection(delta);

            localDirection.x = Mathf.Round(localDirection.x * 10) / 10;
            localDirection.y = Mathf.Round(localDirection.y * 10) / 10;
            localDirection.z = Mathf.Round(localDirection.z * 10) / 10;

            if (localDirection.x > 0 && localDirection.z > 0)
            {
                applyModelData(MotionModel.Acceleration.forwardRight);
            }
            else if (localDirection.x > 0 && localDirection.z < 0)
            {
                applyModelData(MotionModel.Acceleration.backwardRight);
            }
            else if (localDirection.x < 0 && localDirection.z > 0)
            {
                applyModelData(MotionModel.Acceleration.forwardLeft);
            }
            else if (localDirection.x < 0 && localDirection.z < 0)
            {
                applyModelData(MotionModel.Acceleration.backwardLeft);
            }
            else if (localDirection.x == 0)
            {
                applyModelData(localDirection.z > 0 ? MotionModel.Acceleration.forward : MotionModel.Acceleration.backward);
            }
            else if (localDirection.z == 0)
            {
                applyModelData(localDirection.x > 0 ? MotionModel.Acceleration.right : MotionModel.Acceleration.left);
            }
        }

        void applyHeadingChanges()
        {
            float verticalDelta = Input.GetAxis("Horizontal");
            if (verticalDelta != 0)
            {
                applyModelData(Mathf.Sign(verticalDelta) > 0 ? MotionModel.Rotation.right : MotionModel.Rotation.left);
                Quaternion deltaRotation = Quaternion.Euler(angleVelocity * Time.deltaTime * verticalDelta);
                playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);
            }
            else
            {
                applyModelData(MotionModel.Rotation.stale);
            }
        }

        void applyVelocityChanges()
        {
            float horizontalDelta = Input.GetAxis("Vertical");
            Vector3 heading = gameObject.transform.rotation * Vector3.forward;
            Vector3 currVelocity = playerRigidbody.velocity;
            float frameDeltaVelocity = Model.accel * Time.deltaTime;

            if (Mathf.Abs(horizontalDelta) > 0.3f)
            {
                applyModelData(horizontalDelta > 0 ? MotionModel.Acceleration.forward : MotionModel.Acceleration.backward);

                if (Vector3.Angle(heading, currVelocity) > 0)
                {
                    Vector3 desiredVelocity = heading.normalized * Model.maxSpeed * Mathf.Sign(horizontalDelta);
                    Vector3 delta = desiredVelocity - currVelocity;
                    float deltaVelocity = delta.magnitude;

                    applyModelAccordingDelta(delta.normalized);

                    playerRigidbody.AddForce(delta.normalized * (deltaVelocity > frameDeltaVelocity ? frameDeltaVelocity : deltaVelocity), ForceMode.VelocityChange);

                    frameDeltaVelocity = deltaVelocity > frameDeltaVelocity ? 0 : (frameDeltaVelocity - deltaVelocity);
                }

                if (frameDeltaVelocity > 0 && playerRigidbody.velocity.magnitude < Model.maxSpeed)
                {
                    applyModelAccordingDelta(heading);
                    playerRigidbody.AddForce(heading * frameDeltaVelocity * Mathf.Sign(horizontalDelta), ForceMode.VelocityChange);
                }
            }
            else
            {
                float currMagnitude = playerRigidbody.velocity.magnitude;
                if (currMagnitude > Model.cruisingSpeed || (decelerateWithoutControls && currMagnitude > 0))
                {
                    Vector3 decelerateDirection = -1 * playerRigidbody.velocity.normalized;

                    applyModelAccordingDelta(decelerateDirection);

                    Vector3 velocityToAdd = decelerateDirection * Model.accel * Time.deltaTime;

                    playerRigidbody.AddForce(velocityToAdd.magnitude > currMagnitude ? (-1 * playerRigidbody.velocity) : velocityToAdd, ForceMode.VelocityChange);
                }
                else
                {
                    applyModelData(MotionModel.Acceleration.stale);
                }
            }
            Debug.DrawLine(transform.position, playerRigidbody.velocity + transform.position, Color.red);
            //Debug.Log(playerRigidbody.velocity);
        }
    }
}