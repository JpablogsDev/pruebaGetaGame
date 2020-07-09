using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.Track;
using TMPro;

namespace KartGame.UI
{
    public class UIAppearenceEditor : MonoBehaviour
    {
        [SerializeField]
        private CustomPartTag currnPartEdit;

        [Header("Components")]
        [Tooltip("Kart obj to customize")]
        [SerializeField]
        private KartAppearence kartObj;
        [Tooltip("Button to start customization")]
        [SerializeField]
        private UIButton_view btnEdit;
        [Tooltip("Button to close personalization panels")]
        [SerializeField]
        private UIButton_view btnClose;
        [Tooltip("Parent of the edit buttons (hat, body and wheels)")]
        [SerializeField]
        private UITweenScale_view parentButtonsEdit;
        [Tooltip("Parent of color selection buttons")]
        [SerializeField]
        private UITweenScale_view parentButtonsColors;
        [Tooltip("Pather of the selection buttons new models")]
        [SerializeField]
        private UITweenScale_view parentButtonsObj;
        [Tooltip("Interaction Lock Background Image")]
        [SerializeField]
        private RectTransform backgroundBlocker;
        [Tooltip("Text for part currently being edited")]
        [SerializeField]
        private TextMeshProUGUI txtTitle;
        [Tooltip("Current customization")]
        [SerializeField]
        private KartApparenceSetup currAppeareceKart;

        private EditApparence appearenceKartAssets;
        private int indexAssets;
        private int indexColorAssets;

        // ----------------------------------------------------------------------------------------------------


        // ----------------------------------------------------------------------------------------------------

        // Initialize the car customization editor for UI
        public void InitCustomEditorKartUI(EditApparence appearenceKartAssets, KartApparenceSetup currAppeareceKart)
        {
            this.appearenceKartAssets = appearenceKartAssets;
            this.currAppeareceKart = currAppeareceKart;

            kartObj.InitKartAppearence();

            BtnInvokeOnCloseEditorKart();
            currnPartEdit = CustomPartTag.Hat;
        }

        public KartApparenceSetup GetCurrentSetup()
        {
            return currAppeareceKart;
        }

        // --------------------------------------------------------------------------------- Customization

        // Button event for change obj on kart
        public void BtnInvokeOnNextItem(int state)
        {
            // For next and previous item to show
            if (state == 1)
            {
                indexAssets++;
            }
            else
            {
                indexAssets--;
            }

            // Change Hat Obj
            if (currnPartEdit == CustomPartTag.Hat)
            {
                if (indexAssets >= appearenceKartAssets.kartApparence.hat.Count)
                {
                    indexAssets = 0;
                }
                else if (indexAssets < 0)
                {
                    indexAssets = appearenceKartAssets.kartApparence.hat.Count - 1;
                }

                kartObj.SetHatObj(appearenceKartAssets.kartApparence.hat[indexAssets].prefabHat);

                // Save local hat model
                currAppeareceKart.hat = appearenceKartAssets.kartApparence.hat[indexAssets];
                currAppeareceKart.isChangedHat = true;
            }

            // Change Body Obj
            else if (currnPartEdit == CustomPartTag.Body)
            {
                // Not implemented yet
            }
            // Change Wheel Objs
            else if (currnPartEdit == CustomPartTag.Wheel)
            {
                // Not implemented yet
            }
            else
            {
                Debug.Log("Error editing kart: Obj");
            }
        }

        // Button event for change color
        public void BtnInvokeOnNextItemColor(int state)
        { 
            // For next and previous color to show
            if (state == 1)
            {
                indexColorAssets++;
            }
            else
            {
                indexColorAssets--;
            }

            // Change Hat color
            if (currnPartEdit == CustomPartTag.Hat)
            {
                // Not implemented yet
            }

            // Change Body kart color
            else if (currnPartEdit == CustomPartTag.Body)
            {
                if (indexColorAssets >= appearenceKartAssets.kartApparence.color.Count)
                {
                    indexColorAssets = 0;
                }
                else if (indexColorAssets < 0)
                {
                    indexColorAssets = appearenceKartAssets.kartApparence.color.Count - 1;                    
                }

                kartObj.SetBodyColor(appearenceKartAssets.kartApparence.color[indexColorAssets].material);

                // Save local body color
                currAppeareceKart.bodyColor = appearenceKartAssets.kartApparence.color[indexColorAssets].material;
                currAppeareceKart.isChangedBody = true;
            }

            // Change wheel color
            else if (currnPartEdit == CustomPartTag.Wheel)
            {
                if (indexColorAssets >= appearenceKartAssets.kartApparence.color.Count)
                {
                    indexColorAssets = 0;
                }
                else if (indexColorAssets < 0)
                {
                    indexColorAssets = appearenceKartAssets.kartApparence.color.Count - 1;
                }
                kartObj.SetWheelColor(appearenceKartAssets.kartApparence.color[indexColorAssets].material);

                // Save local wheel color
                currAppeareceKart.wheelColor = appearenceKartAssets.kartApparence.color[indexColorAssets].material;
                currAppeareceKart.isChangedWheel = true;
            }
            else
            {
                Debug.Log("Error editing kart: Color");
            }
        }

        // Button event for change part to customize
        public void BtnInvokeOnChangePartEdit(int tag)
        {
            if (tag == (int)CustomPartTag.Hat)
            {
                currnPartEdit = CustomPartTag.Hat;
                parentButtonsColors.gameObject.SetActive(false);
                parentButtonsObj.gameObject.SetActive(true);
            }
            else if (tag == (int)CustomPartTag.Body)
            {
                currnPartEdit = CustomPartTag.Body;
                parentButtonsColors.gameObject.SetActive(true);
                parentButtonsObj.gameObject.SetActive(false);
            }
            else if (tag == (int)CustomPartTag.Wheel)
            {
                currnPartEdit = CustomPartTag.Wheel;
                parentButtonsColors.gameObject.SetActive(true);
                parentButtonsObj.gameObject.SetActive(false);
            }
            else
            {
                currnPartEdit = CustomPartTag.Hat;
                parentButtonsColors.gameObject.SetActive(false);
            }

            // Update text for part to customize
            txtTitle.text = currnPartEdit.ToString();
        }

        // Show controls buttons for customization kart
        public void BtnInvokeOnOpenEditorKart()
        {
            // initialize index for colors and objs
            indexAssets = -1;
            indexColorAssets = -1;

            // Update text for part to customize
            txtTitle.text = currnPartEdit.ToString();

            // Enable and disable objects for kart's customization
            parentButtonsEdit.gameObject.SetActive(true);
            parentButtonsEdit.Reset();
            parentButtonsEdit.PlayForward();

            btnEdit.gameObject.SetActive(false);
            btnClose.gameObject.SetActive(true);
            backgroundBlocker.gameObject.SetActive(true);            

        }

        // Hide controls buttons for customization kart
        public void BtnInvokeOnCloseEditorKart()
        {
            // Enable and disable objects for kart's customization
            parentButtonsEdit.gameObject.SetActive(false);
            btnEdit.gameObject.SetActive(true);
            btnClose.gameObject.SetActive(false);
            backgroundBlocker.gameObject.SetActive(false);
            parentButtonsColors.gameObject.SetActive(false);
        }

    }

}
