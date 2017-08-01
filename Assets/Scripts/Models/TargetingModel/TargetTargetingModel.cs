using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class TargetTargetingModel : TargetingModel
    {
        public Transform target;

        protected override TargetData getTargetData()
        {
            return new TargetData(target.position, target.GetComponent<Rigidbody>().velocity);
        }
    }
}