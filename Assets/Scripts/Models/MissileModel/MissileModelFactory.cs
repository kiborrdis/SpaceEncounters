using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    [CreateAssetMenu(menuName = "SO/MissileModelFactory")]
    public class MissileModelFactory : ModelFactory<MissileModel>
    {
        public MotionModelFactory motionModelFactory;

        public TrajectorySolver solver;
        public GameObject warheadEffectPrefab;

        public float detonationDistance;
        public float detonationAngleThreshold = 180.0f;

        public override MissileModel Model()
        {
            MissileModel model = new MissileModel();

            model.solver = solver;
            model.warheadEffectPrefab = warheadEffectPrefab;
            model.detonationDistance = detonationDistance;
            model.detonationAngleThreshold = detonationAngleThreshold;
            model.motionModel = motionModelFactory.Model();

            return model;
        }
    }
}
