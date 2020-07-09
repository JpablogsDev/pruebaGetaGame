using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.Track
{
    public enum GameMode
    {
        TimeLimit = 0,
        Laps = 1,
        Crash = 2
    }

    public enum CustomPartTag
    {
        Hat,
        Body,
        Wheel,
        Count
    }

    public enum ColorTag
    {        
        Black,
        Blue,
        Green,
        Red,
        White,
        MetalCooper,
        MetalSilver,
        NeonBlue,
        NeonGlow,
        NeonGreen,
        Base,
        Count
    }

    public enum HatTag
    {
        None,
        Party,
        Top,
        Wizard,
        Count
    }

    public enum BodyKartTag
    {
        None,
        Party,
        Top,
        Wizard,
        Count
    }

    public enum WheelKartTag
    {
        None,
        Party,
        Top,
        Wizard,
        Count
    }

    // ----------------------------------------------------------------------------------------------------------

    [System.Serializable]
    public class KartApparenceSetup
    {
        public bool isChangedHat;
        public HatAppareceModel hat;
        public Material hatColor;
        public bool isChangedBody;
        public BodyKartAppareceModel body;
        public Material bodyColor;
        public bool isChangedWheel;
        public WheelAppareceModel wheel;
        public Material wheelColor;

    }

    [System.Serializable]
    public class KartApparenceModel
    {
        public List<HatAppareceModel> hat;
        public List<ColorAppareceModel> color;
        public List<BodyKartAppareceModel> body;
        public List<WheelAppareceModel> wheel;
    }

    [System.Serializable]
    public class HatAppareceModel
    {
        [SerializeField, HideInInspector] private string name;
        public HatTag hatTag;
        public GameObject prefabHat;

        public void Edit_SetName()
        {
            name = (hatTag).ToString();
        }
    }

    [System.Serializable]
    public class ColorAppareceModel
    {
        [SerializeField, HideInInspector] private string name;
        public ColorTag colorTag;
        public Material material;

        public void Edit_SetName()
        {
            name = (colorTag).ToString();
        }
    }

    [System.Serializable]
    public class BodyKartAppareceModel
    {
        [SerializeField, HideInInspector] private string name;
        public BodyKartTag bodyTag;
        public GameObject prefabKart;

        public void Edit_SetName()
        {
            name = (bodyTag).ToString();
        }
    }

    [System.Serializable]
    public class WheelAppareceModel
    {
        [SerializeField, HideInInspector] private string name;
        public WheelKartTag wheelTag;
        public GameObject prefabWheel;

        public void Edit_SetName()
        {
            name = (wheelTag).ToString();
        }
    }

}