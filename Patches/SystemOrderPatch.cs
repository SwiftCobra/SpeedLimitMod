using SpeedLimitEditor.System;
using Game;
using Game.Common;
using HarmonyLib;

namespace SpeedLimitEditor.Patches 
{
    [HarmonyPatch(typeof(SystemOrder))]
    public static class SystemOrderPatch
    {
        [HarmonyPatch("Initialize")]
        [HarmonyPostfix]
        public static void Postfix(UpdateSystem updateSystem)
        {
            updateSystem.UpdateAt<SpeedLimitEditorUISystem>(SystemUpdatePhase.UIUpdate);
        }
    }
}