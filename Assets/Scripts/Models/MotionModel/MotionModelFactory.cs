using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    [CreateAssetMenu(menuName = "SO/MotionModelFactory")]
    public class MotionModelFactory : ModelFactory<MotionModel>
    {

        public float maxSpeed = 5.0f;
        public float cruisingSpeed = 5.0f;
        public float rotationSpeed = 10.0f;
        public float accel = 0.1f;

        public override MotionModel Model()
        {
            MotionModel model = new MotionModel();

            model.maxSpeed = maxSpeed;
			model.cruisingSpeed = cruisingSpeed;
			model.rotationSpeed = rotationSpeed;
			model.accel = accel;
            model.RotationSpeed = rotationSpeed * Mathf.Deg2Rad;

            return model;
        }
    }
}