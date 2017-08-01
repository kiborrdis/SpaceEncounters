using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    [CreateAssetMenu(menuName = "SO/HealthModelFactory")]
    public class HealthModelFactory : ModelFactory<HealthModel>
    {
        public int startHealth;

        public override HealthModel Model()
        {
            HealthModel model = new HealthModel();

            model.health = startHealth;

            return model;
        }

    }
}
