using UnityEngine;

namespace Maze.Services
{
    public class SaveService
    {
        public void SaveResult(float time, float distance)
        {
            PlayerPrefs.SetFloat("LastTime", time);
            PlayerPrefs.SetFloat("LastDistance", distance);
            PlayerPrefs.Save();
        }
    }
}