using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class MissileController : GenericController<MissileModel, BaseView, MissileController>, IModelHolder<MotionModel>
    {
        private GameObject target;
        private Rigidbody objectRigidbody;
        private Rigidbody targetRigidBody;

        public GameObject Target
        {
            get
            {
                return target;
            }

            set
            {
                target = value;

                targetRigidBody = value.GetComponent<Rigidbody>();
            }
        }

        MotionModel IModelHolder<MotionModel>.Model
        {
            get
            {
                return Model.motionModel;
            }

            set
            {
                throw new NotImplementedException();
            }
        }


        // Use this for initialization
        void Awake()
        {
            objectRigidbody = GetComponent<Rigidbody>();

            if (TargetingManager.Instance)
            {
                GameObject targetCandidate = TargetingManager.Instance.Target;
                
                if (targetCandidate)
                {
                    Target = targetCandidate;
                }
            }
        }

        void Detonate()
        {
            GameObject warheadEffect = Instantiate(Model.warheadEffectPrefab, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (!target)
            {
                return;
            }

            if (Vector3.Distance(transform.position, target.transform.position) < Model.detonationDistance)
            {
                Detonate();

                return;
            }

            TrajectorySolver.Trajectory trajectory = Model.solver.calculateTrajectoryFromTo(transform.position, objectRigidbody.velocity, target.transform.position, targetRigidBody.velocity, Model.motionModel.maxSpeed);

            Debug.DrawLine(transform.position, trajectory.endPoint, Color.cyan);

            Model.motionModel.DesiredVelocity = trajectory.velocity;
            Model.motionModel.DesiredHeading = trajectory.velocity.normalized;
        }
    }
}