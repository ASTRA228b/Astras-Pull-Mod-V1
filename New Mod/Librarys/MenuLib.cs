using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class MenuLib
{
    private static Dictionary<string, bool> dropdownStates = new Dictionary<string, bool>();

    private static GUIStyle? mainButton, optionButton, boxStyle;
    private static Texture2D? MainBTex, OptionBTex;
    private static Color ButtonColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    private static void Init()
    {
        if (mainButton != null) return;
        MainBTex = MakeTex(1, 1, ButtonColor);
        OptionBTex = MakeTex(1, 1, ButtonColor);

        mainButton = new GUIStyle(GUI.skin.button);
        mainButton.fontSize = 14;
        mainButton.fixedHeight = 30;
        mainButton.normal.background = MainBTex;
        mainButton.active.background = MainBTex;
        mainButton.hover.background = MainBTex;
        mainButton.focused.background = MainBTex;
        mainButton.onNormal.background = MainBTex;
        mainButton.onActive.background = MainBTex;
        mainButton.onHover.background = MainBTex;
        mainButton.onFocused.background = MainBTex;
        mainButton.normal.textColor = Color.white;
        mainButton.hover.textColor = Color.blue;
        mainButton.active.textColor = Color.red;
        mainButton.focused.textColor = Color.white;
        mainButton.onNormal.textColor = Color.blue;
        mainButton.onHover.textColor = Color.blue;
        mainButton.onActive.textColor = Color.blue;
        mainButton.onFocused.textColor = Color.blue;
        optionButton = new GUIStyle(GUI.skin.button);
        optionButton.fontSize = 12;
        optionButton.fixedHeight = 22;
        optionButton.normal.background = OptionBTex;
        optionButton.active.background = OptionBTex;
        optionButton.hover.background = OptionBTex;
        optionButton.focused.background = OptionBTex;
        optionButton.onNormal.background = OptionBTex;
        optionButton.onActive.background = OptionBTex;
        optionButton.onHover.background = OptionBTex;
        optionButton.onFocused.background = OptionBTex;
        optionButton.normal.textColor = Color.white;
        optionButton.hover.textColor = Color.blue;
        optionButton.active.textColor = Color.red;
        optionButton.focused.textColor = Color.white;
        optionButton.onNormal.textColor = Color.blue;
        optionButton.onHover.textColor = Color.blue;
        optionButton.onActive.textColor = Color.blue;
        optionButton.onFocused.textColor = Color.blue;
        boxStyle = new GUIStyle(GUI.skin.box);
    }

    public static int Dropdown(string id, string[] options, int selectedIndex, params GUILayoutOption[] layout)
    {
        Init();

        if (!dropdownStates.ContainsKey(id))
            dropdownStates[id] = false;


        if (GUILayout.Button(options[selectedIndex], mainButton, layout))
        {
            dropdownStates[id] = !dropdownStates[id];
        }


        if (dropdownStates[id])
        {
            GUILayout.BeginVertical(boxStyle);

            for (int i = 0; i < options.Length; i++)
            {
                if (GUILayout.Button(options[i], optionButton))
                {
                    selectedIndex = i;
                    dropdownStates[id] = false;
                }
            }

            GUILayout.EndVertical();
        }

        return selectedIndex;
    }
    private static Texture2D MakeTex(int WW, int HH, Color Col)
    {
        Texture2D VALL = new Texture2D(WW, HH);
        VALL.SetPixel(0, 0, Col);
        VALL.Apply();
        return VALL;
    }
}
