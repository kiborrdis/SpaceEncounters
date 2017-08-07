using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class HealthModel : Model
    {
        public int health = 100;
        public bool alive = true;

        public List<string> vulnerableTo;
    }
}
