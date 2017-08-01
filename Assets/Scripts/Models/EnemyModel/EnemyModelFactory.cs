using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    [CreateAssetMenu(menuName = "SO/EnemyModelFactory")]
    public class EnemyModelFactory : ModelFactory<EnemyModel>
    {
        public MotionModelFactory motionModel;
        public TargetingModelFactory targetingModel;

        public float fireDistance = 100.0f;
        public float disengageDistance = 50.0f;

        public bool disengageOnAproach = false;
        public bool stopOnAproach = true;

        public override EnemyModel Model()
        {
            EnemyModel model = new EnemyModel();

            model.motionModel = motionModel.Model();
            model.targetingModel = targetingModel.Model();
            model.fireDistance = fireDistance;
            model.disengageDistance = disengageDistance;
            model.disengageOnAproach = disengageOnAproach;
            model.stopOnAproach = stopOnAproach;

            return model;
        }
    }
}
