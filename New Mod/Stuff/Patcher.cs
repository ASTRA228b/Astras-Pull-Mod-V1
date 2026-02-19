using HarmonyLib;

namespace Astras_PullMod.Stuff;

public class Patcher
{
    public static void Apply()
    {
        Harmony VALL = new Harmony(Constantss.GUID);
        VALL.PatchAll();
    }
}