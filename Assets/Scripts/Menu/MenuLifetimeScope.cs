using VContainer;
using VContainer.Unity;

public class MenuLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<SceneLoader>(Lifetime.Singleton);
        builder.Register<MenuUIController>(Lifetime.Singleton);
    }
}