using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Solvers/Collision")]
public class CollisionSolver : TrajectorySolver
{
    public override Trajectory calculateTrajectoryFromTo(Vector3 position, Vector3 velocity, Vector3 targetPosition, Vector3 targetVelocity, float maxSpeed)
    {
        return findCollisionTrajectory(position, velocity, targetPosition, targetVelocity, maxSpeed);
    }

    Trajectory findCollisionTrajectory(Vector3 position, Vector3 velocity, Vector3 targetPosition, Vector3 targetVelocity, float maxSpeed)
    {
        targetVelocity.x -= velocity.x;
        targetVelocity.z -= velocity.z;

        float dx = targetPosition.x - position.x;
        float dy = targetPosition.z - position.z;

        float k = targetVelocity.x * dy - targetVelocity.z * dx;

        float a = dx * dx + dy * dy;
        float b = 2 * k * dx;
        float c = k * k - dy * dy * maxSpeed * maxSpeed;

        float D = b * b - 4 * a * c;

        if (D < 0)
        {
            Debug.Log("D is lower than zero!");
            return new Trajectory();
        }

        float r1 = (-b + Mathf.Sqrt(D)) / (2 * a);
        float r2 = (-b - Mathf.Sqrt(D)) / (2 * a);
        float vmy;

        if (targetPosition.z - position.z > 0)
        {
            vmy = r1;
        }
        else
        {
            vmy = r2;
        }

        float t = dy / (vmy - targetVelocity.z);
        float vmx = (dx + targetVelocity.x * t) / t;
        Vector3 collisionVelocity = new Vector3(vmx, 0, vmy);
        Vector3 collisionPoint = new Vector3(position.x + (vmx + velocity.x) * t, 0, position.z + (vmy + velocity.z) * t);

        return new Trajectory(collisionVelocity, collisionPoint, 0);
    }


}
