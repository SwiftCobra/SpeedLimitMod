using System.Reflection;
using Colossal.Collections;
using SpeedLimitEditor.System;
using Game;
using Game.Common;
using Game.Simulation;
using Game.Tools;
using HarmonyLib;
using Unity.Jobs;
using Unity.Mathematics;

namespace SpeedLimitEditor.Patches 
{
    [HarmonyPatch(typeof(SystemOrder))]
    public static class SystemOrderPatch
    {
        [HarmonyPatch("Initialize")]
        [HarmonyPostfix]
        public static void Postfix(UpdateSystem updateSystem)
        {
            updateSystem.UpdateAt<SpeedLimitEditorSystem>(SystemUpdatePhase.ToolUpdate);
        }
    }

    //[HarmonyPatch(typeof(ValidationSystem))]
    //public static class ValidationSystemPatch
    //{
    //    [HarmonyPatch("OnUpdate")]
    //    [HarmonyPrefix]
    //    public static bool Prefix()
    //    {
    //        return false;
    //    }
    //}

    ////Turn on instant build
    //[HarmonyPatch(typeof(ZoneSpawnSystem), "OnCreate")]
    //class ZoneSpawnSystem_OnCreate
    //{
    //    static void Prefix(ZoneSpawnSystem __instance)
    //    {
    //        __instance.debugFastSpawn = true;
    //    }
    //}
}