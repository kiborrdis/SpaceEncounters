using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class FieldOfViewView : GenericView<FieldOfViewModel, FieldOfViewController>
    {
        private CameraModel cameraModel;

        public override void setModel(FieldOfViewModel model)
        {
            base.setModel(model);
        }

        protected override void Start()
        {
            base.Start();

            cameraModel = Camera.main.GetComponent<CameraModel>();
        }

        private float getScaleMultiplier()
        {
            if (!cameraModel)
            {
                return 1.0f;
            }

            float scaleMultiplier = 1 * cameraModel.CurrentScale;

            return scaleMultiplier < 1 ? 1 : scaleMultiplier;
        }

        void LateUpdate()
        {
            foreach (FieldOfViewUnit unit in Model.units)
            {
                DrawFieldOfView(unit.transform, 360, unit.revealRadius, unit);
            }
        }

        void DrawFieldOfView(Transform owner, float viewAngle, float viewDistance, FieldOfViewUnit unit)
        {
            if (Model.disableRendering)
            {
                return;
            }

            int stepCount = Mathf.RoundToInt(viewAngle * Model.meshResolution);
            float stepAngleSize = viewAngle / stepCount;
            List<Vector3> viewPoints = new List<Vector3>();
            List<float> viewAngles = new List<float>();
            ViewCastInfo oldViewCast = new ViewCastInfo();
            float startAngle = viewAngle == 360.0f ? 0 : owner.eulerAngles.y;

            for (int i = 0; i <= stepCount; i++)
            {
                float angle = startAngle - viewAngle / 2 + stepAngleSize * i;
                ViewCastInfo newViewCast = ViewCast(owner, angle, viewDistance);

                if (i > 0)
                {
                    bool edgeDstThreshholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > Model.edgeDistanceThreshhold;
                    if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && edgeDstThreshholdExceeded))
                    {
                        EdgeInfo edge = FindEdge(owner, oldViewCast, newViewCast, viewDistance);

                        if (edge.pointA != Vector3.zero)
                        {
                            viewPoints.Add(owner.InverseTransformPoint(edge.pointA));
                            viewAngles.Add(edge.angleA);
                        }

                        if (edge.pointB != Vector3.zero)
                        {
                            viewPoints.Add(owner.InverseTransformPoint(edge.pointB));
                            viewAngles.Add(edge.angleB);
                        }
                    }
                }

                viewPoints.Add(owner.InverseTransformPoint(newViewCast.point));
                viewAngles.Add(angle);
                oldViewCast = newViewCast;
            }

            if (Model.ribbon && unit.RibbonViewMesh)
            {
                renderRibbon(viewPoints, viewAngles, unit.RibbonViewMesh, owner);
            }
            //Debug.Log("IT" + fovUnit.ViewMesh + " " + fan);
            if (Model.fan && unit.ViewMesh)
            {
                renderFan(viewPoints, viewAngles, unit.ViewMesh, owner);
            }
        }

        List<Vector3> renderRibbon(List<Vector3> viewPoints, List<float> viewAngles, Mesh viewMesh, Transform owner)
        {
            List<Vector3> innerPoints = new List<Vector3>();
            int vertexCount = viewPoints.Count * 2;
            int cycleCount = viewPoints.Count;
            Vector3[] verticies = new Vector3[vertexCount];
            int[] triangles = new int[vertexCount * 3];

            for (int i = 0; i < cycleCount; i++)
            {
                Vector3 left;
                Vector3 right;
                Vector3 point = viewPoints[i];

                if (i >= viewPoints.Count - 1)
                {
                    right = viewPoints[0];
                    left = viewPoints[i - 1];
                }
                else if (i == 0)
                {
                    right = viewPoints[1];
                    left = viewPoints[viewPoints.Count - 1];
                }
                else
                {
                    right = viewPoints[i + 1];
                    left = viewPoints[i - 1];
                }

                Vector3 temp = left - right;
                Vector3 normal = new Plane(Vector3.zero, temp, Vector3.up).normal;
                normal.Normalize();
                Vector3 ribbonPoint;
                // Debug.Log(fovModel.borderWidth * getScaleMultiplier());

                if (Vector3.Distance(normal + point, Vector3.zero) > Vector3.Distance(normal - point, Vector3.zero))
                {
                    ribbonPoint = normal * Model.borderWidth * getScaleMultiplier() - point;
                }
                else
                {
                    ribbonPoint = normal * Model.borderWidth * getScaleMultiplier() + point;
                }
                innerPoints.Add(ribbonPoint);



                verticies[i] = (point.magnitude - 0.05f) * point.normalized;
                verticies[i + cycleCount] = ribbonPoint;

                if (i < cycleCount - 1)
                {
                    triangles[(i + cycleCount) * 3] = i + cycleCount;
                    triangles[(i + cycleCount) * 3 + 1] = i;
                    triangles[(i + cycleCount) * 3 + 2] = i + 1;

                    triangles[(i) * 3] = i + 1;
                    triangles[(i) * 3 + 1] = i + cycleCount + 1;
                    triangles[(i) * 3 + 2] = i + cycleCount;
                }
            }

            viewMesh.Clear();
            viewMesh.vertices = verticies;
            //viewMesh.uv = uvs;
            viewMesh.triangles = triangles;
            viewMesh.RecalculateNormals();

            return innerPoints;

        }

        void renderFan(List<Vector3> viewPoints, List<float> viewAngles, Mesh viewMesh, Transform owner)
        {

            int vertexCount = viewPoints.Count + 1;
            Vector3[] verticies = new Vector3[vertexCount];
            //Vector2[] uvs = new Vector2[verticies.Length];
            int[] triangles = new int[vertexCount * 3];
            //uvs[0] = new Vector2(0.5f, 0.5f);

            //for (int i = 0; i < vertexCount; i++)
            //{
            //    float uvAngle = viewAngles[i];
            //    float uvRadAngle = uvAngle * Mathf.Deg2Rad;

            //    float x = Mathf.Cos(uvRadAngle) / 2 + 0.5f;
            //    float y = Mathf.Sin(uvRadAngle) / 2 + 0.5f;

            //    Vector2 mainVector = new Vector2(x, y);

            //    uvs[i + 1] = mainVector;

            //}

            verticies[0] = Vector3.zero;
            //Debug.Log(vertexCount + " " + (cycleCount * 3 + cycleCount * 6));

            for (int i = 0; i < vertexCount - 1; i++)
            {

                Vector3 point = viewPoints[i];

                //Debug.Log(point);

                verticies[i + 1] = point;


                if (i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }

            viewMesh.Clear();
            viewMesh.vertices = verticies;
            //viewMesh.uv = uvs;
            viewMesh.triangles = triangles;
            viewMesh.RecalculateNormals();
        }

        EdgeInfo FindEdge(Transform owner, ViewCastInfo min, ViewCastInfo max, float viewDistance)
        {
            float minAngle = min.angle;
            float maxAngle = max.angle;
            Vector3 minPoint = min.point;
            Vector3 maxPoint = max.point;

            for (int i = 0; i < Model.edgeResolveIterations; i++)
            {
                float angle = (minAngle + maxAngle) / 2;
                ViewCastInfo newViewCast = ViewCast(owner, angle, viewDistance);

                bool edgeDstThreshholdExceeded = Mathf.Abs(min.dst - newViewCast.dst) > Model.edgeDistanceThreshhold;
                if (newViewCast.hit == min.hit && !edgeDstThreshholdExceeded)
                {
                    minAngle = angle;
                    minPoint = newViewCast.point;
                }
                else
                {
                    maxAngle = angle;
                    maxPoint = newViewCast.point;
                }

            }

            return new EdgeInfo(minPoint, maxPoint, minAngle, maxAngle);
        }

        ViewCastInfo ViewCast(Transform owner, float globalAngle, float viewDistance)
        {
            Vector3 dir = DirFromAngle(globalAngle, true);

            RaycastHit hit;
            if (Physics.Raycast(owner.position, dir, out hit, viewDistance, Model.obstacleMask))
            {
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }
            else
            {
                return new ViewCastInfo(false, owner.position + dir * viewDistance, viewDistance, globalAngle);
            }
        }

        Vector3 DirFromAngle(float angle, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                Debug.Log("There is some bullshit");
                angle += transform.eulerAngles.y;
            }
            float radAngle = angle * Mathf.Deg2Rad;

            return new Vector3(Mathf.Sin(radAngle), 0, Mathf.Cos(radAngle));
        }

        public struct ViewCastInfo
        {
            public bool hit;
            public Vector3 point;
            public float dst;
            public float angle;

            public ViewCastInfo(bool hit, Vector3 point, float dst, float angle)
            {
                this.hit = hit;
                this.point = point;
                this.dst = dst;
                this.angle = angle;
            }
        }

        public struct EdgeInfo
        {
            public Vector3 pointA;
            public Vector3 pointB;
            public float angleA;
            public float angleB;

            public EdgeInfo(Vector3 pointA, Vector3 pointB, float angleA, float angleB)
            {
                this.pointA = pointA;
                this.pointB = pointB;
                this.angleA = angleA;
                this.angleB = angleB;
            }
        }
    }
}
