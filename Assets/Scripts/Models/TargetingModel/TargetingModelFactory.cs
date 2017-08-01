using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    [CreateAssetMenu(menuName = "SO/TargetingModelFactory")]
    public class TargetingModelFactory : ModelFactory<TargetingModel>
    {
        public enum TargetingType { Point, Target };

        public TargetingType targetingType;

        public override TargetingModel Model()
        {
            TargetingModel model;

            if (targetingType == TargetingType.Point )
            {
                model = new PointTargetingModel();
            } else
            {
                model = new TargetTargetingModel();
            }

            return model;
        }
    }
}
