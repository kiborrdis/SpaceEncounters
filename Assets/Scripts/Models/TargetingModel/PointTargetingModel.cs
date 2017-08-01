using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class PointTargetingModel : TargetingModel
    {
        public Vector3 point = Vector3.zero;

        protected override TargetData getTargetData()
        {
            return new TargetData(point, Vector3.zero);
        }
    }
}