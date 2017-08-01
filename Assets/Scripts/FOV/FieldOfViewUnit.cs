using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    public class FieldOfViewUnit : GenericController<FieldOfViewModel, BaseView, FieldOfViewUnit>
    {
        public float revealRadius = 4.0f;

        public MeshFilter viewMeshFilter;
        public MeshFilter ribbonViewMeshFilter;
        Mesh viewMesh;
        Mesh ribbonMesh;

        public Mesh ViewMesh
        {
            get
            {
                return viewMesh;
            }
        }

        public Mesh RibbonViewMesh
        {
            get
            {
                return ribbonMesh;
            }
        }

        protected override void Start()
        {
            
            base.Start();

            if (Model == null)
            {
                Debug.Log("FieldOfViewUnit: Model is not specified");
            }

            if (viewMeshFilter)
            {
                viewMesh = new Mesh();
                viewMesh.name = "View mesh";

                viewMeshFilter.mesh = viewMesh;
            }

            if (ribbonViewMeshFilter)
            {
                ribbonMesh = new Mesh();
                ribbonMesh.name = "View mesh";

                ribbonViewMeshFilter.mesh = ribbonMesh;
            }

            Model.units.Add(this);
        }

        private void OnDestroy()
        {
            if (Model != null)
            {
                //Debug.Log("This is fucking wrong!");
            }

            Model.units.Remove(this);
        }
    }
}