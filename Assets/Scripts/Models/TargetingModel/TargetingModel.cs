using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class TargetingModel : Model
    {
        protected List<TurretModel> turrets = new List<TurretModel>();
        protected ITarget target;

        public LayerMask allyLayer;

        public ITarget Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;

                foreach (TurretModel model in turrets)
                {
                    model.target = value;
                }
            }
        }

        public virtual void AllowFreeFire()
        {
            foreach (TurretModel model in turrets)
            {
                model.allowFire = true;
            }
        }

        public virtual void AllowNumberOfShots(int shotsNumber)
        {
            if (turrets.Count == 0)
            {
                return;
            }

            int shotsPerTurret = shotsNumber / turrets.Count;
            int restShots = shotsNumber % turrets.Count;

            foreach (TurretModel model in turrets)
            {
                int currentTurretShots = shotsPerTurret;

                if (restShots > 0)
                {
                    currentTurretShots += 1;
                    restShots -= 1;
                }

                model.numberOfAllowedShots = currentTurretShots;
            }
        }

        public virtual void StopFire()
        {
            foreach (TurretModel model in turrets)
            {
                model.allowFire = false;
                model.numberOfAllowedShots = 0;
            }
        }
        
        public void registerTurret(TurretModel turretModel)
        {
            turrets.Add(turretModel);
        }

        public void unregisterTurret(TurretModel turretModel)
        {
            turrets.Remove(turretModel);
        }
    }
}
