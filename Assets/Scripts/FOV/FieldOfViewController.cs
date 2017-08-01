using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class FieldOfViewController : GenericController<FieldOfViewModel, FieldOfViewView, FieldOfViewController>
    {
        IEnumerator FindTargetsWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                Model.FindVisibleTargets();
            }
        }

        protected override void Start()
        {
            base.Start();

            StartCoroutine("FindTargetsWithDelay", 0.2f);
        }
    }
}
