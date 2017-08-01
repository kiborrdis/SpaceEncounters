using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpaceEncounter
{
    [CustomEditor(typeof(TurretView))]
    public class TurretViewEditor : Editor
    {
        void OnSceneGUI()
        {
            //TurretView turretView = (TurretView)target;

            //Handles.color = Color.magenta;
            //Handles.DrawLine(turretView.transform.position, turretView.transform.position + turretView.Model.CurrentHeading * 20);

            //Handles.color = Color.red;
            //Handles.DrawLine(turretView.transform.position, turretView.transform.position + turretView.Model.DesiredHeading * 20);

            //Handles.color = Color.white;
            //Handles.DrawLine(turretView.transform.position, turretView.transform.position + turretView.Model.OriginHeading * 20);

        }
    }
}