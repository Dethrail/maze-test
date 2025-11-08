using Maze;
using Maze.Core;
using Maze.Maze;
using UnityEngine;
using VContainer;
using VContainer.Unity;


public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private int mazeWidth = 10;
    [SerializeField] private int mazeHeight = 10;

    [SerializeField] private GameCanvas canvas;
    [SerializeField] private MazeRoot mazeRoot;

    protected override void Configure(IContainerBuilder builder)
    {
        // load and bind game settings
        var gameSettings = (GameSettings)Resources.Load("Settings/GameSettings");
        builder.RegisterInstance(gameSettings).AsImplementedInterfaces();

        // Core services
        builder.Register<SaveService>(Lifetime.Singleton);
        builder.Register<SceneLoader>(Lifetime.Singleton);

        // Maze
        var generator = new PrimsMazeGenerator();
        builder.RegisterInstance(generator).AsImplementedInterfaces();
        builder.RegisterInstance(canvas).AsImplementedInterfaces();
        builder.RegisterInstance(mazeRoot).AsImplementedInterfaces();
        // Container.InjectGameObject(mazeRoot.gameObject);
        
        // Player
        builder.Register<PlayerInput>(Lifetime.Singleton);
        builder.Register<PlayerController>(Lifetime.Singleton);

        // UI
        builder.Register<Timer>(Lifetime.Singleton).AsSelf().As<ITickable>();
        builder.Register<HUDController>(Lifetime.Transient);
        
        builder.RegisterEntryPoint<GamePresenter>();
    }
}