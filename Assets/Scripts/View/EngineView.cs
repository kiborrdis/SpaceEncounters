using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineView : MonoBehaviour {

    public enum EngineDirection { forward, backward, leftfront, rightfront, leftback, rightback };

    static class EngineData
    {
        public static Dictionary<EngineDirection, MotionModel.Acceleration[]> accelerations = new Dictionary<EngineDirection, MotionModel.Acceleration[]>();
        public static Dictionary<EngineDirection, MotionModel.Rotation[]> rotations = new Dictionary<EngineDirection, MotionModel.Rotation[]>();

        static EngineData()
        {
            accelerations.Add(EngineDirection.forward, new MotionModel.Acceleration[]{ MotionModel.Acceleration.backward });
            accelerations.Add(EngineDirection.backward, new MotionModel.Acceleration[] { MotionModel.Acceleration.forward });
            accelerations.Add(EngineDirection.leftfront, new MotionModel.Acceleration[] { MotionModel.Acceleration.right });
            accelerations.Add(EngineDirection.leftback, new MotionModel.Acceleration[] { MotionModel.Acceleration.right });
            accelerations.Add(EngineDirection.rightfront, new MotionModel.Acceleration[] { MotionModel.Acceleration.left });
            accelerations.Add(EngineDirection.rightback, new MotionModel.Acceleration[] { MotionModel.Acceleration.left });

            rotations.Add(EngineDirection.forward, new MotionModel.Rotation[] {  });
            rotations.Add(EngineDirection.backward, new MotionModel.Rotation[] {  });
            rotations.Add(EngineDirection.leftfront, new MotionModel.Rotation[] { MotionModel.Rotation.right });
            rotations.Add(EngineDirection.leftback, new MotionModel.Rotation[] { MotionModel.Rotation.left });
            rotations.Add(EngineDirection.rightfront, new MotionModel.Rotation[] { MotionModel.Rotation.left });
            rotations.Add(EngineDirection.rightback, new MotionModel.Rotation[] { MotionModel.Rotation.right });
        }
    }

    public EngineDirection direction;

    ParticleSystem particleView;

    MotionModel model;

    void Start () {
        particleView = GetComponent<ParticleSystem>();

        model = transform.parent.GetComponent<MotionModel>();

        if (!model)
        {
            Debug.Log("Engine's parent should have MotionModel component");
        }
    }

    private void Update()
    {
        if (!model)
        {
            return;
        }

        handleModelData();
    }

    void handleModelData ()
    {
        if (System.Array.Exists(EngineData.accelerations[direction], testAccel => testAccel == model.acceleration) || System.Array.Exists(EngineData.rotations[direction], testRotation => testRotation == model.rotation))
        {
            Burn();
        } else
        {
            Stale();
        }
    }

    public void Burn ()
    {
        particleView.Play();
    }

    public void Stale ()
    {
        particleView.Stop();
    }
}
