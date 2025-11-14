using Maze.Services;
using VContainer;
using VContainer.Unity;

namespace Maze.Menu
{
    public class MenuLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<MenuUIController>(Lifetime.Singleton);
        }
    }
}