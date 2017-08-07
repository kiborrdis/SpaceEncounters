using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    [CreateAssetMenu(menuName = "SO/TargetingModelFactory")]
    public class TargetingModelFactory : ModelFactory<TargetingModel>
    {
        public LayerMask allyLayer;

        public override TargetingModel Model()
        {
            TargetingModel model = new TargetingModel();

            model.allyLayer = allyLayer;

            return model;
        }
    }
}
