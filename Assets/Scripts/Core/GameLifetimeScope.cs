using System;
using Maze.Interfaces;
using Maze.Maze;
using Maze.Player;
using Maze.Services;
using Maze.Settings;
using Maze.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Maze.Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private int mazeWidth = 10;
        [SerializeField] private int mazeHeight = 10;

        [SerializeField] private GameCanvas canvas;
        [SerializeField] private MazeRoot mazeRoot;
        [SerializeField] private Camera mainCamera;

        protected override void Configure(IContainerBuilder builder)
        {
            // load and bind game settings
            var gameSettings = (GameSettings)Resources.Load("Settings/GameSettings");
            builder.RegisterInstance(gameSettings).AsImplementedInterfaces();

            // Core services
            builder.Register<RuntimeData>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SaveService>(Lifetime.Singleton);
            builder.Register<GameStateService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SceneLoader>(Lifetime.Singleton);

            // Maze
            IMazeGenerator generator;
            if (PlayerPrefs.GetString(PrefsConstraints.ALGORITHM) == PrefsConstraints.PRIMS)
            {
                generator = new PrimsMazeGenerator();
                builder.RegisterInstance(generator);
            }
            else if (PlayerPrefs.GetString(PrefsConstraints.ALGORITHM) == PrefsConstraints.KRUSKAL)
            {
                generator = new KruskalMazeGenerator();
                builder.RegisterInstance(generator);
            }
            else
            {
                throw new Exception("Meze generator isn't selected");
            }

            builder.RegisterInstance(canvas).AsImplementedInterfaces();
            builder.RegisterInstance(mazeRoot).AsImplementedInterfaces();
            builder.RegisterInstance(mainCamera);

            // Player
            builder.Register<PlayerInput>(Lifetime.Singleton);
            builder.Register<PlayerController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.RegisterEntryPoint<MazeController>(Lifetime.Singleton);
            builder.RegisterEntryPoint<UiService>(Lifetime.Singleton);
        }
    }
}