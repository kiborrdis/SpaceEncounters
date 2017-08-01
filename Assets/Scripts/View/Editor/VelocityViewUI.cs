using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpaceEncounter
{
    [CustomEditor(typeof(VelocityIndicatorView))]
    public class VelocityViewUI : Editor
    {

        void OnSceneGUI()
        {
            VelocityIndicatorView velocityView = (VelocityIndicatorView)target;

            Handles.color = Color.white;
            Handles.DrawWireArc(velocityView.transform.position, Vector3.up, Vector3.forward, 360, velocityView.radius);
        }
    }
}