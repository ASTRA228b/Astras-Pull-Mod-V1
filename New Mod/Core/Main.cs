using Astras_PullMod.Librarys;
using GorillaLocomotion;
using UnityEngine;
using UnityEngine.InputSystem;




namespace Astras_PullMod.Core;

public class Main : MonoBehaviour
{
    private Rect Window = new Rect(155, 155, 360, 460);
    private bool Open = false;
    private bool StylesLoded = false;
    private bool dropdownOpen = false;
    private Texture2D? Windowtex, Background, Slidertex, SliderThumbtex;
    private GUIStyle? WindowStyle, Buttonss, SliderStyle, SliderThumbStyle;
    private Color WindowColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    private Color ButtonColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    private Color sliderTrackColor = new Color(0.15f, 0.15f, 0.15f, 1f);
    private Color sliderThumbColor = new Color(0.0f, 0.6f, 1f, 1f);
    private static float pullPower = 0f;
    private static bool lasttouchleft;
    private static bool lasttouchright;
    public enum HandPullMode
    {
        Left,
        Right,
        Both
    }
    public static HandPullMode CurrentMode = HandPullMode.Both;

    private void OnGUI()
    {
        if (!StylesLoded)
        {
            Windowtex = MakeTex(1, 1, WindowColor);
            Background = MakeTex(1, 1, ButtonColor);
            Slidertex = MakeTex(1, 1, sliderTrackColor);
            SliderThumbtex = MakeTex(1, 1, sliderThumbColor);
            WindowStyle = new GUIStyle(GUI.skin.window);
            WindowStyle.normal.background = Windowtex;
            WindowStyle.hover.background = Windowtex;
            WindowStyle.active.background = Windowtex;
            WindowStyle.focused.background = Windowtex;
            WindowStyle.onNormal.background = Windowtex;
            WindowStyle.onHover.background = Windowtex;
            WindowStyle.onActive.background = Windowtex;
            WindowStyle.onFocused.background = Windowtex;
            WindowStyle.normal.textColor = Color.white;
            WindowStyle.fontStyle = FontStyle.Normal;
            Buttonss = new GUIStyle(GUI.skin.button);
            Buttonss.normal.background = Background;
            Buttonss.active.background = Background;
            Buttonss.hover.background = Background;
            Buttonss.focused.background = Background;
            Buttonss.onNormal.background = Background;
            Buttonss.onActive.background = Background;
            Buttonss.onHover.background = Background;
            Buttonss.onFocused.background = Background;
            Buttonss.normal.textColor = Color.white;
            Buttonss.hover.textColor = Color.blue;
            Buttonss.active.textColor = Color.red;
            Buttonss.focused.textColor = Color.white;
            Buttonss.onNormal.textColor = Color.blue;
            Buttonss.onHover.textColor = Color.blue;
            Buttonss.onActive.textColor = Color.blue;
            Buttonss.onFocused.textColor = Color.blue;
            SliderStyle = new GUIStyle(GUI.skin.horizontalSlider);
            SliderThumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
            SliderStyle.normal.background = Slidertex;
            SliderStyle.active.background = Slidertex;
            SliderStyle.hover.background = Slidertex;
            SliderThumbStyle.normal.background = SliderThumbtex;
            SliderThumbStyle.active.background = SliderThumbtex;
            SliderThumbStyle.hover.background = SliderThumbtex;
            StylesLoded = true;
        }
        if (Open)
        {
            Window.height = dropdownOpen ? 520 : 460;
            Window = GUILayout.Window(987065, Window, UIM, "Astras PullMod V1", WindowStyle);
        }
    }

    private void UIM(int id)
    {
        Mod();
        GUILayout.Space(10f);
        if (GUILayout.Button("Close", Buttonss))
        {
            Open = !Open;
        }
        GUI.DragWindow();
    }

    private void Mod()
    {
        GUILayout.Label("Change PullSpeed:");
        pullPower = GUILayout.HorizontalSlider(pullPower, 0.001f, 0.200f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Speed set to {pullPower:F3}");

        GUILayout.Space(5f);

        GUILayout.Label("Change Input:");

        int oldIndex = InputSelector.SelectedIndex;
        InputSelector.SelectedIndex = MenuLib.Dropdown(
            "pullmod_input",
            InputSelector.InputNames,
            InputSelector.SelectedIndex,
            GUILayout.Width(200)
        );

        dropdownOpen = oldIndex != InputSelector.SelectedIndex;

        GUILayout.Label($"Current input: {InputSelector.InputNames[InputSelector.SelectedIndex]}");
        GUILayout.Space(5f);
        GUILayout.Label("Presets:");
        if (GUILayout.Button("Speed Boost Setting: (25)", Buttonss))
        {
            pullPower = 0.025f;
        }
        if (GUILayout.Button("Legit Setting: (70)", Buttonss))
        {
            pullPower = 0.070f;
        }
        if (GUILayout.Button("Random Setting", Buttonss))
        {
            float RandomSetting = UnityEngine.Random.Range(0.001f, 0.200f);
            pullPower *= RandomSetting;
        }
        if (GUILayout.Button("Reset", Buttonss))
        {
            pullPower = 0.001f;
        }
        GUILayout.Space(5f);
        GUILayout.Label("Hand Options:");
        GUILayout.BeginHorizontal();
        if (GUILayout.Toggle(CurrentMode == HandPullMode.Both, "Both"))
        {
            CurrentMode = HandPullMode.Both;
        }
        GUILayout.Space(5f);
        if (GUILayout.Toggle(CurrentMode == HandPullMode.Left, "Left"))
        {
            CurrentMode = HandPullMode.Left;
        }
        GUILayout.Space(5f);
        if (GUILayout.Toggle(CurrentMode == HandPullMode.Right, "Right"))
        {
            CurrentMode = HandPullMode.Right;
        }
     

        GUILayout.EndHorizontal();
        GUILayout.Label("PullMode: " + CurrentMode.ToString());
    }


    private void Update()
    {

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            Open = !Open;
        }
    }
    private void FixedUpdate()
    {
        PullMod();
    }

    // PullMod
    public static void PullMod()
    {
        bool leftTouching = GTPlayer.Instance.IsHandTouching(true);
        bool rightTouching = GTPlayer.Instance.IsHandTouching(false);
        bool leftReleased = !leftTouching && lasttouchleft;
        bool rightReleased = !rightTouching && lasttouchright;

        if (InputSelector.Pressed)
        {
            bool shouldPull = CurrentMode switch
            {
                HandPullMode.Left => leftReleased,
                HandPullMode.Right => rightReleased,
                HandPullMode.Both => leftReleased || rightReleased,
                _ => false
            };

            if (shouldPull)
            {
                Vector3 vel = GorillaTagger.Instance.rigidbody.linearVelocity;
                GTPlayer.Instance.transform.position += new Vector3(vel.x * pullPower, 0f, vel.z * pullPower);
            }
        }

        lasttouchleft = leftTouching;
        lasttouchright = rightTouching;
    }




    private Texture2D MakeTex(int WW, int HH, Color Col)
    {
        Texture2D VALL = new Texture2D(WW, HH);
        VALL.SetPixel(0, 0, Col);
        VALL.Apply();
        return VALL;
    }
}