using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    [CreateAssetMenu(menuName = "SO/FOVModel")]
    public class FieldOfViewModelFactory : ModelFactory<FieldOfViewModel>
    {
        public LayerMask targetMask;
        public LayerMask obstacleMask;

        public float meshResolution = 0.4f;
        public int edgeResolveIterations = 10;
        public float edgeDistanceThreshhold = 0.5f;
        public float borderDist = 0.2f;
        public float borderWidth = 1.0f;
        public bool disableRendering = false;
        public bool ribbon = false;
        public bool fan = true;
        public int framesBetweenUpdate = 3;

        private FieldOfViewModel model;

        private void Build() {
            model = new FieldOfViewModel();

            model.targetMask = this.targetMask;
            model.obstacleMask = this.obstacleMask;
            model.meshResolution = this.meshResolution;
            model.edgeResolveIterations = this.edgeResolveIterations;
            model.edgeDistanceThreshhold = this.edgeDistanceThreshhold;
            model.borderDist = this.borderDist;
            model.borderWidth = this.borderWidth;
            model.disableRendering = this.disableRendering;
            model.ribbon = this.ribbon;
            model.fan = this.fan;
            model.framesBetweenUpdate = this.framesBetweenUpdate;
        }

        public override FieldOfViewModel Model()
        {
            if (model == null)
            {
                Build();
            }

            return model;
        }
    }
}