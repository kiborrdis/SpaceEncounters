using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class MissileModel : Model
    {
        public MotionModel motionModel;

        public TrajectorySolver solver;
        public GameObject warheadEffectPrefab;

        public float detonationDistance;
        public float detonationAngleThreshold = 180.0f;
    }
}
