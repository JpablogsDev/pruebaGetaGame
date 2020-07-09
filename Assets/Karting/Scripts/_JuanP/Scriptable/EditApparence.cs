using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.Track;

namespace KartGame
{
    [CreateAssetMenu(fileName = "NewKartApparecen", menuName = "KartApparence" + "/ApparecenSetup", order = 1)]
    public class EditApparence : ScriptableObject
    {

        [SerializeField]
        public KartApparenceModel kartApparence;

        #if UNITY_EDITOR
        void OnValidate()
        {
            for (int i = 0; i < kartApparence.hat.Count; i++)
            {
                kartApparence.hat[i].Edit_SetName();
            }

            for (int i = 0; i < kartApparence.color.Count; i++)
            {
                kartApparence.color[i].Edit_SetName();
            }

            for (int i = 0; i < kartApparence.body.Count; i++)
            {
                kartApparence.body[i].Edit_SetName();
            }

            for (int i = 0; i < kartApparence.wheel.Count; i++)
            {
                kartApparence.wheel[i].Edit_SetName();
            }

        }
        #endif

    }
}

