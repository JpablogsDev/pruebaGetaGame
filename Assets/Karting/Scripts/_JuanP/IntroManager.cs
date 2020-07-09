using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.UI;
using KartGame.Track;
using Unity.Collections;

namespace KartGame
{
    public class IntroManager : MonoBehaviour
    {
        [Tooltip("Manager for the initial scene UI")]
        [SerializeField]
        private UIIntroManager uiManager;

        private LoadSceneButton loadScene;

        // Start is called before the first frame update
        void Start()
        {
            // Get last kart customization if exist
            KartApparenceSetup currSetupKart = new Track.KartApparenceSetup();
            if (Persistence.currApparenceKart != null)
            {
                currSetupKart = Persistence.currApparenceKart;
            }
            else
            {
                currSetupKart = new KartApparenceSetup();
            }           

            // Get LoadScenebutton component
            loadScene = GetComponent<LoadSceneButton>();

            // inititlize UI manager
            uiManager.InitUIIntroManager(currSetupKart);
            uiManager.onStartGameplay -= OnStartGameplay;
            uiManager.onStartGameplay += OnStartGameplay;
        }


        // Call back to load gameplay scenes according to game mode
        private void OnStartGameplay(int gameMode, KartApparenceSetup appearenceSetup)
        {
            Persistence.currApparenceKart = appearenceSetup;

            Persistence.currGameMode = (KartGame.Track.GameMode)gameMode;
            if (loadScene != null)
            {
                if (Persistence.currGameMode == Track.GameMode.Laps)
                {
                    loadScene.LoadSceneModeLap();
                }
                else if (Persistence.currGameMode == Track.GameMode.TimeLimit)
                {
                    loadScene.LoadSceneModeTime();
                }
                else
                {
                    loadScene.LoadSceneModeTime();
                }                
            }
        }

    }
}

