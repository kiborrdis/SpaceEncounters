using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class HealthView : GenericView<HealthModel, HealthController>
    {
        public GameObject hitPrefab;
        public GameObject destroyPrefab;

        private void OnTriggerEnter(Collider other)
        {
            if (!Model.vulnerableTo.Contains(other.tag))
            {
                return;
            }

            Instantiate(hitPrefab, other.transform.position, other.transform.rotation);
            Controller.onHit(1);

            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }

        public void onHealthZero()
        {
            Instantiate(destroyPrefab, transform.position, transform.rotation);
        }
    }
}