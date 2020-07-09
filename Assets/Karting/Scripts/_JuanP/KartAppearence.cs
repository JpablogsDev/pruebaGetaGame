using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame
{
    public class KartAppearence : MonoBehaviour
    {
        [SerializeField]
        private SkinnedMeshRenderer bodyKart;

        [SerializeField]
        private Transform pivotHat;

        [SerializeField]
        private List<MeshRenderer> wheelKart;

        // ------------------------------------------------------------------
        // initialize last appearence setup
        public void InitKartAppearence()
        {
            if (Persistence.currApparenceKart != null)
            {
                if (Persistence.currApparenceKart.isChangedBody)
                {
                    // Set body color
                    SetBodyColor(Persistence.currApparenceKart.bodyColor);
                }
                if (Persistence.currApparenceKart.isChangedHat)
                {
                    // Set hat obj
                    SetHatObj(Persistence.currApparenceKart.hat.prefabHat);
                }
                if (Persistence.currApparenceKart.isChangedWheel)
                {
                    // Set wheel colors
                    SetWheelColor(Persistence.currApparenceKart.wheelColor);
                }
                
            }
        }
        // ------------------------------------------------------------------
        public void SetBody(GameObject body)
        {

        }

        public void SetBodyColor(Material materialColor)
        {
            bodyKart.material = materialColor;
        }

        // ------------------------------------------------------------------
        public void SetHatObj(GameObject hat)
        {
            // Clear childs
            if (pivotHat.childCount > 0)
            {
                for (int i = 0; i < pivotHat.childCount; i++)
                {
                    Destroy(pivotHat.GetChild(0).gameObject);
                }
            }
            if (hat != null)
            {
                // Instante new hat for kart
                Instantiate(hat, pivotHat.transform);
            }
        }

        public void SetHatColor(Material materialColor)
        {
            // Not implemented yet
        }

        // ------------------------------------------------------------------
        public void SetWheelObj(GameObject wheel)
        {
            // Not implemented yet
        }

        public void SetWheelColor(Material materialColor)
        {
            for (int i = 0; i < wheelKart.Count; i++)
            {
                wheelKart[i].material = materialColor;
            }           
        }
    }
}
