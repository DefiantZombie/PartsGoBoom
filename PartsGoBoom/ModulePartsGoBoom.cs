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

        // LEGACY DO NOT TOUCH
        [KSPAction("#SSC_PartsGoBoom_000001",
            advancedTweakable = true, requireFullControl = false)]
        public void ActivateFTS(KSPActionParam param)
        {
            part.explode();
        }

        [KSPField(guiName = "#SSC_PartsGoBoom_000003", isPersistant = true,
            guiActiveEditor = true, guiActive = true)]
        [UI_ChooseOption(scene = UI_Scene.Editor)]
        public string FTSMode = "Single";

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
            if (FTSMode == "Single")
            {
                part.explode();
            }
            else if(FTSMode == "Symmetry")
            {
                FTSMulti(part.symmetryCounterparts);
                part.explode();
            }
            else if(FTSMode == "Vessel")
            {
                FTSMulti(vessel.Parts, new Part[] { part });
                part.explode();
            }
        }

        public void FTSMulti(List<Part> parts, Part[] exclude = null)
        {
            IEnumerable<Part> partsEx = exclude == null ? parts.ToArray() : parts.Except(exclude).ToArray();
            foreach (Part p in partsEx)
            {
                if (p.Modules.GetModule<ModulePartsGoBoom>() == null) continue;

                p.explode();
            }
        }

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            this.UpdateUIChooseOption(nameof(FTSMode), ModeOptions, PartsGoBoom.LocalStrings.Values.ToArray(), true, FTSMode);
        }
    }
}
