using Astras_PullMod.Librarys;

namespace Astras_Pull_Mod_V1.Core.GUIHelpers;

public static class InputSelector
{
    public static string[] InputNames =
    {
        "Right Grab",
        "Left Grab",
        "Right Trigger",
        "Left Trigger",
        "RightJoyStick (Hold)",
        "LeftjoySrick (Hold)",
        "A Button",
        "B Button",
        "X Button",
        "Y Button"
    };

    public static Func<bool>[] Inputs =
    {
        () => InputLib.RightGrab,
        () => InputLib.LeftGrab,
        () => InputLib.RightTrigger,
        () => InputLib.LeftTrigger,
        () => InputLib.RightJoystickClick, 
        () => InputLib.LeftJoystickClick,
        () => InputLib.RightControllerAButton,
        () => InputLib.RightControllerBButton,
        () => InputLib.LeftControllerXButton,
        () => InputLib.LeftControllerYButton
    };

    public static int SelectedIndex = 0;

    public static bool Pressed => Inputs[SelectedIndex]?.Invoke() ?? false;

}