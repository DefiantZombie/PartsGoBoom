namespace PartsGoBoom
{
    public class ModulePartsGoBoom : PartModule
    {
        [KSPAction("#SSC_PartsGoBoom_000001",
            advancedTweakable = true, requireFullControl = false)]
        public void ActivateFTS(KSPActionParam param)
        {
            ActivateFTS();
        }

        [KSPEvent(active = true, advancedTweakable = true,
            guiActive = true, guiActiveEditor = false,
            guiActiveUncommand = true, guiActiveUnfocused = true,
            requireFullControl = false, guiName = "#SSC_PartsGoBoom_000001")]
        public void ActivateFTS()
        {
            part.explode();
        }
    }
}
