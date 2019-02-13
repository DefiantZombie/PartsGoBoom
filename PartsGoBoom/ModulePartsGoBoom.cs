namespace PartsGoBoom
{
    public class ModulePartsGoBoom : PartModule
    {
        [KSPAction("#SSC_PartsGoBoom_000001",
            advancedTweakable = true, requireFullControl = false)]
        public void ActivateFTS(KSPActionParam param)
        {
            part.explode();
        }
    }
}
