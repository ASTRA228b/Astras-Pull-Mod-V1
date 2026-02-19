using UnityEngine;
using BepInEx;
using Astras_PullMod.Stuff;
using Astras_PullMod.Core;

namespace Astras_PullMod.Loader;

[BepInPlugin(Constantss.GUID, Constantss.Name, Constantss.Version)]
public class Plugin : BaseUnityPlugin
{
    private void Start()
    {
        Patcher.Apply();
    }

    private void Awake()
    {
        GameObject PluginOBJ = new GameObject(Constantss.ObjectName);
        PluginOBJ.AddComponent<Main>();
        DontDestroyOnLoad(PluginOBJ);
    }
}