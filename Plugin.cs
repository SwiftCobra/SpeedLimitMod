using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using HookUILib.Core;
using UnityEngine;
#if BEPINEX_V6
    using BepInEx.Unity.Mono;
#endif

namespace SpeedLimitEditor
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

            var harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MyPluginInfo.PLUGIN_GUID + "_Cities2Harmony");
            var patchedMethods = harmony.GetPatchedMethods().ToArray();

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} made patches! Patched methods: " + patchedMethods.Length);

            foreach (var patchedMethod in patchedMethods) {
                Logger.LogInfo($"Patched method: {patchedMethod.DeclaringType?.FullName}:{patchedMethod.Name}");
            }
        }

        // Keep in mind, Unity UI is immediate mode, so OnGUI is called multiple times per frame
        // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnGUI.html
        private void OnGUI() {
            GUI.Label(new Rect(10, 10, 300, 20), $"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }

    public class SpeedLimitEditorUI : UIExtension
    {
        public new readonly string extensionID = "scobra.speed_limit_editor";
        public new readonly string extensionContent;
        public new readonly ExtensionType extensionType = ExtensionType.Panel;

        public SpeedLimitEditorUI()
        {
            this.extensionContent = this.LoadEmbeddedResource("SpeedLimitEditor.dist.bundle.js");
        }
    }
}
