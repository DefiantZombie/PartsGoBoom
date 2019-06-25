using System;
using System.Collections.Generic;
using UnityEngine;
using KSP.Localization;

namespace PartsGoBoom
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class PartsGoBoom : MonoBehaviour
    {
        public enum Strings
        {
            Single,
            Symmetry,
            Vessel
        }

        public static Dictionary<Strings, string> LocalStrings;

        public static event Action OnLanguageSwitchedEvent;

        private void Awake()
        {
            LocalStrings = new Dictionary<Strings, string>();

            OnLanguageSwitched();
        }

        private void Start()
        {
            GameEvents.onLanguageSwitched.Add(OnLanguageSwitched);
        }

        private void OnLanguageSwitched()
        {
            LocalStrings[Strings.Single] = Localizer.Format("#SSC_PartsGoBoom_000004");
            LocalStrings[Strings.Symmetry] = Localizer.Format("#SSC_PartsGoBoom_000005");
            LocalStrings[Strings.Vessel] = Localizer.Format("#SSC_PartsGoBoom_000006");

            OnLanguageSwitchedEvent?.Invoke();
        }

        private void OnDestroy()
        {
            GameEvents.onLanguageSwitched.Remove(OnLanguageSwitched);
        }
    }
}
