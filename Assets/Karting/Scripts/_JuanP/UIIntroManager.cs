using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KartGame.Track;

namespace KartGame.UI
{
    public class UIIntroManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private UIAppearenceEditor appearenceEditor;

        [Header("Scriptable")]
        [SerializeField]
        private EditApparence appearenceKartAssets;

        // ---------------------------------------------------------------- Delegates

        // Delegate for when a game mode is selected from UI (GameMode Tag)
        public System.Action<int, KartApparenceSetup> onStartGameplay;

        // --------------------------------------------------------------------------

        // Initilize UI manager for intro scene
        public void InitUIIntroManager(KartApparenceSetup currAppeareceKart)
        {  
            appearenceEditor.InitCustomEditorKartUI(appearenceKartAssets, currAppeareceKart);
        }

        // Button event for select game mode
        public void BtnInvokeOnStarGameMode(int gameMode)
        {
            onStartGameplay?.Invoke(gameMode, appearenceEditor.GetCurrentSetup());
        }

    }
}

