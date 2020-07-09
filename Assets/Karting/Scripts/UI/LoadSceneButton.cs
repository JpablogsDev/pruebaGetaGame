using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("Initial scene name")]
        public string SceneIntro;

        [Tooltip("Game mode scene name by laps")]
        public string SceneNameLap;

        [Tooltip("Time - limited game mode scene name")]
        public string SceneNameTime;

        public void LoadSceneModeLap() 
        {
            SceneManager.LoadSceneAsync(SceneNameLap);
        }

        public void LoadSceneModeTime()
        {
            SceneManager.LoadSceneAsync(SceneNameTime);
        }

        public void LoadSceneIntro()
        {
            SceneManager.LoadSceneAsync(SceneIntro);
        }
    }
}
