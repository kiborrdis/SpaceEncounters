namespace SpaceEncounter
{
    public class EnemyModel : Model
    {
        public MotionModel motionModel;
        public TargetingModel targetingModel;

        public float fireDistance = 100.0f;
        public float disengageDistance = 50.0f;

        public bool disengageOnAproach = false;
        public bool stopOnAproach = true;
    }
}
