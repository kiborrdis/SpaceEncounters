using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    //TODO: Optimize this stuff
    public class TurretModel : Model
    {

        public TurretModel(float rotationSpeedDeg, float arcAngleDeg, Vector3 originHeading)
        {
            this.originHeading = originHeading;
            currentHeading = originHeading;
            idealDesiredHeading = Vector3.zero;
            realDesiredHeading = Vector3.zero;
            currentRotation = Quaternion.FromToRotation(originHeading, currentHeading);


            rotationSpeed = rotationSpeedDeg * Mathf.Deg2Rad;
            arcAngle = arcAngleDeg * Mathf.Deg2Rad;
        }

        public GameObject projectile;
        public float cooldown = 10.0f;
        public float shootingAngleThreshold = 5.0f;
        public float power = 100.0f;
        public float hittingRange = 100.0f;

        public bool allowFire = false;
        public int numberOfAllowedShots = 0;

        public ITarget target;

        private Vector3 currentHeading;
        private Vector3 originHeading;

        private float rotationSpeed;
        private float arcAngle;
        private Quaternion currentRotation;

        private Vector3 idealDesiredHeading;
        private Vector3 realDesiredHeading;

        private Vector3 possitiveBorder;
        private Vector3 negativeBorder;

        private void calculateBorderHeadings()
        {
            possitiveBorder = Quaternion.Euler(0, arcAngle * Mathf.Rad2Deg, 0) * originHeading;
            negativeBorder = Quaternion.Euler(0, -arcAngle * Mathf.Rad2Deg, 0) * originHeading;
        }

        private void calculateRealHeading()
        {
            if (Vector3.Angle(originHeading, idealDesiredHeading) > arcAngle * Mathf.Rad2Deg)
            {
                realDesiredHeading = Vector3.Angle(possitiveBorder, idealDesiredHeading) > Vector3.Angle(negativeBorder, idealDesiredHeading) ? negativeBorder : possitiveBorder;

                return;
            }

            realDesiredHeading = idealDesiredHeading;
        }

        public float RotationSpeed {
            get
            {
                return rotationSpeed;
            }
        }

        public Vector3 OriginHeading
        {
            get
            {
                return originHeading;
            }
            set
            {
                originHeading = value;
                currentHeading = currentRotation * value;
                calculateBorderHeadings();
                calculateRealHeading();
            }
        }

        public Vector3 DesiredHeading
        {
            get
            {
                return realDesiredHeading;
            }

            set
            {
                idealDesiredHeading = value;
                calculateRealHeading();
            }
        }

        public Vector3 CurrentHeading
        {
            get
            {
                return currentHeading;
            }

            set
            {
                currentHeading = value;
                currentRotation = Quaternion.FromToRotation(originHeading, currentHeading);
            }
        }

        public bool AllowFire
        {
            get
            {
                if (numberOfAllowedShots > 0)
                {
                    numberOfAllowedShots -= 1;
                    return true;
                }

                return allowFire;
            }
        }

        
    }
}
