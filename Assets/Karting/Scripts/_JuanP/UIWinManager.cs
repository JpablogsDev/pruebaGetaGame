using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.UI
{
    public class UIWinManager : MonoBehaviour
    {
        [SerializeField]
        private LoadSceneButton loadSceneBtn;

        // Reload last game mode scene
        public void BtnInvokeOnReloadLastScene()
        {
            if (Persistence.currGameMode == Track.GameMode.Laps)
            {
                loadSceneBtn.LoadSceneModeLap();
            }
            else if (Persistence.currGameMode == Track.GameMode.TimeLimit)
            {
                loadSceneBtn.LoadSceneModeTime();
            }
            else
            {
                loadSceneBtn.LoadSceneModeLap();
            }

        }

        // Load intro Scene
        public void BtnInvokeOnLoadIntroScene()
        {
            loadSceneBtn.LoadSceneIntro();
        }         


    }

}

