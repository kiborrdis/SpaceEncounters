using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class EngineView : GenericView<MotionModel, BaseController>
    {
        public enum EngineDirection { forward, backward, leftfront, rightfront, leftback, rightback };

        static class EngineData
        {
            public static Dictionary<EngineDirection, MotionModel.Acceleration[]> accelerations = new Dictionary<EngineDirection, MotionModel.Acceleration[]>();
            public static Dictionary<EngineDirection, MotionModel.Rotation[]> rotations = new Dictionary<EngineDirection, MotionModel.Rotation[]>();

            static EngineData()
            {
                accelerations.Add(EngineDirection.forward, new MotionModel.Acceleration[] { MotionModel.Acceleration.backward, MotionModel.Acceleration.backwardRight, MotionModel.Acceleration.backwardLeft });
                accelerations.Add(EngineDirection.backward, new MotionModel.Acceleration[] { MotionModel.Acceleration.forward, MotionModel.Acceleration.forwardRight, MotionModel.Acceleration.forwardLeft });
                accelerations.Add(EngineDirection.leftfront, new MotionModel.Acceleration[] { MotionModel.Acceleration.right, MotionModel.Acceleration.forwardRight, MotionModel.Acceleration.backwardRight });
                accelerations.Add(EngineDirection.leftback, new MotionModel.Acceleration[] { MotionModel.Acceleration.right, MotionModel.Acceleration.forwardRight, MotionModel.Acceleration.backwardRight });
                accelerations.Add(EngineDirection.rightfront, new MotionModel.Acceleration[] { MotionModel.Acceleration.left, MotionModel.Acceleration.forwardLeft, MotionModel.Acceleration.backwardLeft });
                accelerations.Add(EngineDirection.rightback, new MotionModel.Acceleration[] { MotionModel.Acceleration.left, MotionModel.Acceleration.forwardLeft, MotionModel.Acceleration.backwardLeft });

                rotations.Add(EngineDirection.forward, new MotionModel.Rotation[] { });
                rotations.Add(EngineDirection.backward, new MotionModel.Rotation[] { });
                rotations.Add(EngineDirection.leftfront, new MotionModel.Rotation[] { MotionModel.Rotation.right });
                rotations.Add(EngineDirection.leftback, new MotionModel.Rotation[] { MotionModel.Rotation.left });
                rotations.Add(EngineDirection.rightfront, new MotionModel.Rotation[] { MotionModel.Rotation.left });
                rotations.Add(EngineDirection.rightback, new MotionModel.Rotation[] { MotionModel.Rotation.right });
            }
        }

        public EngineDirection direction;

        ParticleSystem particleView;
        AudioSource audioView;

        protected override void Start()
        {
            base.Start();

            particleView = GetComponent<ParticleSystem>();
            audioView = GetComponent<AudioSource>();
            getModelFromParent();
        }

        private void getModelFromParent()
        {
            MotionView motionView = transform.parent.GetComponent<MotionView>();

            Model = motionView.Model;
        }

        private void Update()
        {
            if (Model == null)
            {
                getModelFromParent();
                if (Model == null)
                {
                    Stale();
                    Debug.LogWarning("EngineView: Model is not specified");
                }

                return;
            }

            handleModelData();
        }

        void handleModelData()
        {
            if (System.Array.Exists(EngineData.accelerations[direction], testAccel => testAccel == Model.acceleration) || System.Array.Exists(EngineData.rotations[direction], testRotation => testRotation == Model.rotation))
            {
                Burn();
            }
            else
            {
                Stale();
            }
        }

        public void Burn()
        {
            particleView.Play();

            if (audioView && !audioView.isPlaying)
            {
                audioView.Play();
            }
        }

        public void Stale()
        {
            particleView.Stop();

            if (audioView)
            {
                audioView.Stop();
            }
        }
    }
}
