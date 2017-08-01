using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class TargetingManager : MonoBehaviour
    {

        public LayerMask mask;
        public GameObject selection;
        static TargetingManager _instance;
        GameObject target;


        public static TargetingManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public GameObject Target
        {
            get
            {
                return target;
            }
        }

        // TODO Find a way to do this without a singleton
        void Awake()
        {
            _instance = this;

        }

        void Update()
        {
            //TODO check out if it's possible to use events instead
            if (Input.GetButtonDown("Fire2"))
            {

                Vector3 mousePos = Input.mousePosition;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);

                if (Physics.Raycast(ray, out hit, 10000, mask))
                {
                    Debug.Log("Found target " + hit.collider.gameObject + " " + hit.collider.gameObject.name);
                    if (target)
                    {
                        //TODO get rid of this "name" shit
                        Transform oldSelection = target.transform.Find("Selection");

                        Destroy(oldSelection.gameObject);
                    }

                    target = hit.collider.gameObject;

                    GameObject newSelection = Instantiate(selection, target.transform.TransformPoint(new Vector3(0, 1, 0)), new Quaternion(), target.transform) as GameObject;
                    //TODO get rid of this "name" shit
                    newSelection.name = "Selection";
                }
            }
        }
    }
}