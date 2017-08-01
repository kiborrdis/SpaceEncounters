using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class TurretView : GenericView<TurretModel, TurretController>
    {
        Vector3 originHeading;

        void adjustModel()
        {
            Controller.onOriginChange(transform.parent.parent.TransformDirection(originHeading));
        }

        protected override void Start()
        {
            base.Start();

            originHeading = transform.rotation * Vector3.forward;
        }

        void Update()
        {
            adjustModel();

            transform.rotation = Quaternion.FromToRotation(Vector3.forward, Model.CurrentHeading);
        }
    }
}
