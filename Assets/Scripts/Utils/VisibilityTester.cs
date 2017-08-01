using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEncounter
{
    [CreateAssetMenu(menuName = "SO/Testers/VisibilityTester")]
    public class VisibilityTester : ScriptableObject
    {
        public bool affectedByDetection = true;
        public bool visibleByRadar = false;
        public bool visibleBySight = false;
        public float minVisibleScale = 0.0f;
        public float maxVisibleScale = 100.0f;

        public bool isVisible(bool detectedBySight, bool detectedByRadar, float scale)
        {
            bool visible = true;

            if (affectedByDetection && visibleByRadar && !detectedBySight)
            {
                visible = visible && detectedByRadar;
            } else if (affectedByDetection && visibleByRadar && detectedBySight)
            {
                visible = false;
            }

            if (affectedByDetection && visibleBySight)
            {
                visible = visible && detectedBySight;
            }

            visible = visible && (scale > minVisibleScale && scale <= maxVisibleScale);

            return visible;
        }
    }
}