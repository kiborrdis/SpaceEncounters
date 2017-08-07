using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class TurretController : GenericController<TurretModel, TurretView, TurretController>
    {
        private float beforeNextShot = 0.0f;

        public Object targetingProvider;
        TargetingModel targetingModel;

        protected override void Start()
        {
            base.Start();

            if (targetingProvider != null)
            {
                IModelHolder<TargetingModel> holder = (IModelHolder<TargetingModel>)targetingProvider;

                targetingModel = holder.Model;

                targetingModel.registerTurret(Model);
            }
        }

        public void onOriginChange(Vector3 newOrigin)
        {
            Model.OriginHeading = newOrigin;
        }

        void UpdateDesiredHeading()
        {
            if (Model.target == null)
            {
                return;
            }

            TargetInfo info = Model.target.getTargetInfo();

            Vector3 desiredHeading = info.Location - View.transform.position;

            Model.DesiredHeading = desiredHeading.normalized;
        }

        void UpdateRotation()
        {
            Vector3 desiredHeading = Model.DesiredHeading;
            Vector3 currentHeading = Model.CurrentHeading;
            float rotationSpeed = Model.RotationSpeed;

            if (desiredHeading.magnitude != 0 && Vector3.Angle(currentHeading, desiredHeading) > 0)
            {
                Vector3 nextHeading;

                nextHeading = Vector3.RotateTowards(currentHeading, desiredHeading, rotationSpeed * Time.deltaTime, 1);

                Model.CurrentHeading = nextHeading;
            }
        }

        void TryToFire()
        {
            if (Model.AllowFire && Vector3.Angle(Model.CurrentHeading, Model.DesiredHeading) < Model.shootingAngleThreshold && !Physics.Raycast(View.transform.position + transform.forward, Model.CurrentHeading, Model.hittingRange, targetingModel.allyLayer))
            {
                Fire();
            }
        }

        void Fire()
        {
            if (beforeNextShot > 0)
            {
                return;
            }
            Vector3 pos = View.transform.position + transform.forward;
            pos.y = 0;
            GameObject newProjectile = Instantiate(Model.projectile, pos, Quaternion.Euler(0, 0, 0)) as GameObject;

            if (!newProjectile.GetComponent<Rigidbody>())
            {
                newProjectile.AddComponent<Rigidbody>();
            }

            newProjectile.GetComponent<Rigidbody>().AddForce(Model.CurrentHeading * Model.power, ForceMode.VelocityChange);
            beforeNextShot = Model.cooldown;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateDesiredHeading();
            UpdateRotation();
            TryToFire();

            if (beforeNextShot > 0)
            {
                beforeNextShot -= Time.deltaTime;
            }
        }
    }
}
