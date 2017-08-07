using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceEncounter
{
    public class MovingTargetModel : Model, ITarget
    {
        Transform target;
        Rigidbody targetRigidbody;

        public MovingTargetModel(Transform target)
        {
            this.target = target;

            targetRigidbody = target.GetComponent<Rigidbody>();

            if (!targetRigidbody)
            {
                Debug.LogWarning("Moving target does not have rigidbody");
            }
        }

        public TargetInfo getTargetInfo()
        {
            return new TargetInfo(target.position, targetRigidbody ? targetRigidbody.velocity : Vector3.zero);
        }

        public TargetType getTargetType()
        {
            throw new NotImplementedException();
        }
    }
}
