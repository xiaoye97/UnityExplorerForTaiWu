using System;
using FrameWork;
using HarmonyLib;
using System.Reflection;
using TaiwuModdingLib.Core.Plugin;

namespace UnityExplorerForTaiWu
{
    [PluginConfig("UnityExplorerForTaiWu", "宵夜97", "1.0.0")]
    public class UnityExplorerForTaiWu : TaiwuRemakePlugin
    {
        public override void Dispose()
        {
            
        }

        public override void Initialize()
        {
            try
            {
                LoadDll("UniverseLib.Mono");
                LoadDll("UnityExplorer.STANDALONE.Mono");
                var type = AccessTools.TypeByName("UnityExplorer.ExplorerStandalone");
                Traverse.Create(type).Method("CreateInstance").GetValue();
            }
            catch (Exception ex)
            {
                DialogCmd cmd = new DialogCmd
                {
                    Title = "异常",
                    Content = $"加载UnityExplorer时出现了异常，请在Log文件查看详细信息。",
                    Type = 2,
                    GroupYesText = "好的"
                };
                UIElement.Dialog.SetOnInitArgs(EasyPool.Get<ArgumentBox>().SetObject("Cmd", cmd));
                UIManager.Instance.ShowUI(UIElement.Dialog);
                GLog.Error($"加载UnityExplorer时出现了异常:\n{ex}");
            }
        }

        public Assembly LoadDll(string dllName)
        {
            string dllDirPath = ModManager.GetModRootFolder() + "/UnityExplorerForTaiWu/Plugins";
            string dllPath = dllDirPath + "/" + dllName + ".dll";
            var bytes = System.IO.File.ReadAllBytes(dllPath);
            var a = Assembly.Load(bytes);
            return a;
        }
    }
}