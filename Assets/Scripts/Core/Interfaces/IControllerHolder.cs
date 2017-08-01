namespace SpaceEncounter
{
    public interface IControllerHolder<TController>
    {
        TController Controller { get; set; }
    }
}
