using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceEncounter
{
    public class SimpleEnemyController : 
        GenericController<EnemyModel, BaseView, SimpleEnemyController>,
        IModelHolder<MotionModel>,
        IModelHolder<TargetingModel>
    {
        Rigidbody engine;
        GameObject player;

        TargetTargetingModel targetingModel;

        TargetingModel IModelHolder<TargetingModel>.Model
        {
            get
            {
                return Model.targetingModel;
            }

            set
            {
                throw new NotImplementedException();
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

        protected override void Start()
        {
            base.Start();

            targetingModel = (TargetTargetingModel)Model.targetingModel;

            player = GameObject.FindGameObjectWithTag("Player");
            Transform gunObj = transform.Find("EnemyGun");
            engine = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (!player)
            {
                Debug.LogWarning("No player");
                return;
            }

            targetingModel.target = player.transform;
            targetingModel.UpdateTurretTargeting();

            Vector3 direction = (player.transform.position - transform.position).normalized;
            Vector3 desiredLocation = 0.75f * Model.fireDistance * (-1 * direction) + player.transform.position;
            Vector3 desiredVelocity;

            if (Vector3.Distance(player.transform.position, transform.position) < Model.fireDistance)
            {
                desiredVelocity = Vector3.zero;

                targetingModel.AllowFreeFire();
            }
            else
            {
                desiredVelocity = direction * Model.motionModel.maxSpeed;

                targetingModel.StopFire();
            }
            
            Model.motionModel.DesiredVelocity = desiredVelocity;
            Model.motionModel.DesiredHeading = direction;
        }
    }
}