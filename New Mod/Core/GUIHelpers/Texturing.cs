using UnityEngine;

namespace Astras_Pull_Mod_V1.Core.GUIHelpers;

public static class Texturing
{
    public static Texture2D MakeTextures(int WW, int HH, Color COL)
    {
        Texture2D H = new Texture2D(WW, HH);
        H.SetPixel(0, 0, COL);
        H.Apply();
        return H;
    }
}