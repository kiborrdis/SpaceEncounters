using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceEncounter
{
    [CreateAssetMenu(menuName = "SO/ModelFactories/TurretModelFactory")]
    public class TurretModelFactory : ModelFactory<TurretModel>
    {
        public float rotationSpeed;
        public float arcAngle;

        public GameObject projectile;
        public float cooldown = 10.0f;
        public float shootingAngleThreshold = 5.0f;
        public float power = 100.0f;
        public float hittingRange = 100.0f;

        public override TurretModel Model()
        {
            TurretModel model = new TurretModel(rotationSpeed, arcAngle, Vector3.forward);

            model.cooldown = cooldown;
            model.shootingAngleThreshold = shootingAngleThreshold;
            model.projectile = projectile;
            model.power = power;
            model.hittingRange = hittingRange;

            return model;
        }
    }
}