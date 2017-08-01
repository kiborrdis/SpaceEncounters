using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceEncounter
{
    public class HealthController : GenericController<HealthModel, HealthView, HealthController>
    {

        public void onHit(int hitPoints)
        {
            Model.health -= hitPoints;


            if (Model.alive && Model.health <= 0)
            {
                View.onHealthZero();

                Model.alive = false;

                Destroy(gameObject);
            }
        }
       
    }
}
