using UnityEngine;
using VContainer.Unity;

public class Timer : ITickable
{
    public float TimeElapsed { get; private set; }

    public void Tick()
    {
        TimeElapsed += Time.deltaTime;
    }
}