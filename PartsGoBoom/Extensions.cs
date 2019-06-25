using System;

namespace PartsGoBoom
{
    public static class Extensions
    {
        public static void UpdateUIChooseOption(this PartModule module, string fieldName, string[] options, string[] display, bool forceUpdate, string forceVal = "")
        {
            if (!HighLogic.LoadedSceneIsEditor) return;

            if (display.Length == 0 || options.Length == 0 || display.Length != options.Length)
                return;

            UI_ChooseOption control = (UI_ChooseOption)module.Fields[fieldName].uiControlEditor;
            if (control == null) return;

            module.Fields[fieldName].guiActiveEditor = options.Length > 1;

            control.display = display;
            control.options = options;

            if(forceUpdate && control.partActionItem != null)
            {
                UIPartActionChooseOption pai = (UIPartActionChooseOption)control.partActionItem;

                var t = control.onFieldChanged;
                control.onFieldChanged = null;

                int index = Array.IndexOf(options, forceVal);

                pai.slider.minValue = 0;
                pai.slider.maxValue = options.Length - 1;
                pai.slider.value = index;

                pai.OnValueChanged(index);

                control.onFieldChanged = t;
            }
        }
    }
}
