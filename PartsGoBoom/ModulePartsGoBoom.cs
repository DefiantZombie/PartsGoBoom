using System.Collections.Generic;
using System.Linq;

namespace PartsGoBoom
{
    public class ModulePartsGoBoom : PartModule
    {
        protected string[] ModeOptions = new string[]
        {
            "Single",
            "Symmetry",
            "Vessel"
        };

        [KSPAction("#SSC_PartsGoBoom_000001",
            advancedTweakable = true, requireFullControl = false)]
        public void ActivateFTS(KSPActionParam param)
        {
            FTS();
        }

        [KSPField(guiName = "#SSC_PartsGoBoom_000003", isPersistant = true,
            guiActiveEditor = true, guiActive = true),
            UI_ChooseOption(scene = UI_Scene.Editor)]
        public string ftsMode = "Single";

        [KSPAction("#SSC_PartsGoBoom_000002",
            advancedTweakable = true, requireFullControl = false)]
        public void ActivateNewFTS(KSPActionParam param)
        {
            ActivateNewFTS();
        }

        [KSPEvent(guiName = "#SSC_PartsGoBoom_000002", active = true, 
            advancedTweakable = true, guiActive = true, 
            guiActiveEditor = false, guiActiveUncommand = true, 
            guiActiveUnfocused = true, requireFullControl = false)]
        public void ActivateNewFTS()
        {
            if (ftsMode == "Single")
            {
                FTS();
            }
            else if(ftsMode == "Symmetry")
            {
                FTSMulti(part.symmetryCounterparts);
                FTS();
            }
            else if(ftsMode == "Vessel")
            {
                FTSMulti(vessel.Parts, new Part[] { part });
                FTS();
            }
        }

        public void FTSMulti(List<Part> parts, Part[] exclude = null)
        {
            IEnumerable<Part> partsEx = exclude == null ? parts.ToArray() : parts.Except(exclude).ToArray();
            foreach (Part p in partsEx)
            {
                ModulePartsGoBoom module = p.Modules.GetModule<ModulePartsGoBoom>();
                if (module == null) continue;

                module.FTS();
            }
        }

        public void FTS()
        {
            part.explode();
        }

        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            
            UpdateMenu();

            PartsGoBoom.OnLanguageSwitchedEvent += UpdateMenu;
        }

        protected void UpdateMenu()
        {
            this.UpdateUIChooseOption(nameof(ftsMode), ModeOptions, PartsGoBoom.LocalStrings.Values.ToArray(), true, ftsMode);
        }

        private void OnDestroy()
        {
            PartsGoBoom.OnLanguageSwitchedEvent -= UpdateMenu;
        }
    }
}
