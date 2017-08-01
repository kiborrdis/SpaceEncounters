using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceEncounter
{

public class FieldOfViewModel : Model {

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();
    public List<FieldOfViewUnit> units = new List<FieldOfViewUnit>();

    public float meshResolution = 0.4f;
    public int edgeResolveIterations = 10;
    public float edgeDistanceThreshhold = 0.5f;
    public float borderDist = 0.2f;
    public float borderWidth = 1.0f;
    public bool disableRendering = false;
    public bool ribbon = false;
    public bool fan = true;
    public int framesBetweenUpdate = 3;

    public void FindVisibleTargets()
    {
        visibleTargets.Clear();

        foreach (FieldOfViewUnit unit in units)
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(unit.transform.position, unit.revealRadius, targetMask);
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - unit.transform.position).normalized;
                float dstToTarget = Vector3.Distance(unit.transform.position, target.position);
                RaycastHit obstHit;
                RaycastHit targetHit;

                if (!Physics.Raycast(unit.transform.position, dirToTarget, out obstHit, unit.revealRadius, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
                else
                {
                    Physics.Raycast(unit.transform.position, dirToTarget, out targetHit, unit.revealRadius, targetMask);

                    if (targetHit.collider.transform == target && obstHit.distance > targetHit.distance)
                    {
                        visibleTargets.Add(target);
                    }
                }
            }
        }
    }
}
}