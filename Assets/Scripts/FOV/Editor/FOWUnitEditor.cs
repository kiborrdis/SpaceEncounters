using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfViewUnit))]
public class FOWUnitEditor : Editor
{

    void OnSceneGUI()
    {
        FieldOfViewUnit fow = (FieldOfViewUnit)target;
        foreach (FieldOfViewUnit unit in fow.manager.units)
        {
            Handles.color = Color.yellow;
            if(unit != fow)
                Handles.DrawWireArc(unit.transform.position, Vector3.up, Vector3.forward, 360, unit.revealRadius);
        }
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.revealRadius);

        foreach (Transform target in fow.manager.visibleTargets)
        {
            Handles.color = Color.red;

            Handles.DrawWireCube(target.transform.position, target.transform.localScale * 1.2f);
        }
    }

}
