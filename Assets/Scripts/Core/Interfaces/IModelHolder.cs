namespace SpaceEncounter
{
    public interface IModelHolder<TModel>
    {
        TModel Model { get; set; }
    }
}
