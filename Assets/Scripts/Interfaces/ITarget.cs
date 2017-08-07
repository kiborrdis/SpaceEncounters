namespace SpaceEncounter
{
    public enum TargetType { ship, missile, point };

    public interface ITarget
    {
        TargetInfo getTargetInfo();

        TargetType getTargetType();
    }
}
