using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceEncounter
{
    public class PointTargetModel : Model, ITarget
    {
        private Vector3 location;

        public PointTargetModel(Vector3 location)
        {
            this.location = location;
        }

        public TargetInfo getTargetInfo()
        {
            return new TargetInfo(location);
        }

        public TargetType getTargetType()
        {
            throw new NotImplementedException();
        }
    }
}
