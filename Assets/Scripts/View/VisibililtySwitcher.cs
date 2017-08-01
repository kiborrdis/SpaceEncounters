using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceEncounter
{
    public class VisibililtySwitcher : MonoBehaviour
    {
        public UnityEngine.Object radarModelProvider;
        public UnityEngine.Object sightModelProvider;
        public VisibilityTester visibilityTester;
        
        FieldOfViewModel radarModel;
        FieldOfViewModel sightModel;
        CameraModel cameraModel;

        GameObject rootParent;

        bool visible = true;

        protected TModel GetModelFromProvider<TModel>(UnityEngine.Object potentialProvider)
        {
            if (potentialProvider == null)
            {
                return default(TModel);
            }

            IModelProvider<TModel> modelProvider = (IModelProvider<TModel>)potentialProvider;

            if (modelProvider == null)
            {
                return default(TModel);
            }

            return modelProvider.Model();
        }

        void toggleChildrenTo (bool status)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(status);
            }
        }

        protected void Start()
        {
            radarModel = GetModelFromProvider<FieldOfViewModel>(radarModelProvider);
            sightModel = GetModelFromProvider<FieldOfViewModel>(sightModelProvider);

            cameraModel = Camera.main.GetComponent<CameraModel>();

            rootParent = transform.parent.gameObject;

            while (rootParent.transform.parent != null)
            {
                rootParent = rootParent.transform.parent.gameObject;
            }
        }

        void Update()
        {
            bool detectedByRadar = radarModel != null ? radarModel.visibleTargets.Contains(rootParent.transform) : true;
            bool detectedBySight = sightModel != null ? sightModel.visibleTargets.Contains(rootParent.transform) : true;

            bool currVisibility = visibilityTester.isVisible(detectedBySight, detectedByRadar, cameraModel.CurrentScale);

            if (currVisibility != visible)
            {
                visible = currVisibility;

                toggleChildrenTo(visible);
            }
        }

    }
}