using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class VelocityIndicatorView : GenericView<MotionModel, BaseController>
    {

        public float radius = 1;
        public float scale = 0.1f;

        LineRenderer lineRenderer;
        Vector3[] linePoints = new Vector3[2];

        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            lineRenderer = GetComponent<LineRenderer>();

            MotionView motionView = transform.parent.GetComponent<MotionView>();

            Model = motionView.Model;

            if (!lineRenderer)
            {
                Debug.Log("Line renderer hasnt found");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!lineRenderer || Model == null)
            {
                return;
            }
            Vector3 velocity = transform.InverseTransformVector(Model.Velocity);

            if (velocity.magnitude > 0)
            {
                lineRenderer.enabled = true;

                Vector3 velocityHeading = velocity.normalized;

                linePoints[0] = velocityHeading * radius;
                linePoints[1] = linePoints[0] + velocityHeading * velocity.magnitude * scale;

                lineRenderer.SetPositions(linePoints);
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
    }
}