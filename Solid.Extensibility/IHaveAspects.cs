namespace Solid.Extensibility
{
    public interface IHaveAspects<T>
    {
        T UseAspect(IAspect aspect);
    }
}