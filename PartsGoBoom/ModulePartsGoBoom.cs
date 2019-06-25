using System.Collections.Generic;
using System.Linq;

namespace PartsGoBoom
{
    public class ModulePartsGoBoom : PartModule
    {
        protected readonly string[] ModeOptions = new string[]
        {
            "Single",
            "Symmetry",
            "Vessel"
        };

        [KSPAction("#SSC_PartsGoBoom_000001",
            advancedTweakable = true, requireFullControl = false)]
        public void ActivateFTS(KSPActionParam param)
        {
            ActivateFTS();
        }

        [KSPField(guiName = "#SSC_PartsGoBoom_000002", isPersistant = true,
            guiActiveEditor = true, guiActive = true)]
        [UI_ChooseOption(scene = UI_Scene.Editor)]
        public string FTSMode = "Single";

        [KSPEvent(guiName = "#SSC_PartsGoBoom_000001", active = true, 
            advancedTweakable = true, guiActive = true, 
            guiActiveEditor = false, guiActiveUncommand = true, 
            guiActiveUnfocused = true, requireFullControl = false)]
        public void ActivateFTS()
        {
            if (FTSMode == "Single")
            {
                FTS();
            }
            else if(FTSMode == "Symmetry")
            {
                FTSMulti(part.symmetryCounterparts);
                FTS();
            }
            else if(FTSMode == "Vessel")
            {
                FTSMulti(vessel.Parts);
                FTS();
            }

            UIPartActionController.Instance.Deselect(false);
        }

        public void FTSMulti(List<Part> parts)
        {
            foreach (Part p in parts.ToArray())
            {
                if (p == part) continue;

                ModulePartsGoBoom module = p.Modules.GetModule<ModulePartsGoBoom>();
                if (module == null) continue;

                module.FTS();
            }
        }

        public void FTS()
        {
            part.UnpinPartActionWindow();
            part.explode();
        }

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            this.UpdateUIChooseOption(nameof(FTSMode), ModeOptions, PartsGoBoom.LocalStrings.Values.ToArray(), true, FTSMode);
        }
    }
}
