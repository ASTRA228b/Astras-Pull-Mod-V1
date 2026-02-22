using Valve.VR;

namespace Astras_PullMod.Librarys;

internal class InputLib
{
    public static bool RightGrab => ControllerInputPoller.instance.rightControllerGripFloat > 0.5f;
    public static bool LeftGrab => ControllerInputPoller.instance.leftControllerGripFloat > 0.5f;
    public static bool RightTrigger => ControllerInputPoller.instance.rightControllerTriggerButton;
    public static bool LeftTrigger => ControllerInputPoller.instance.leftControllerTriggerButton;
    // A
    public static bool RightControllerAButton => ControllerInputPoller.instance.rightControllerPrimaryButton;
    // B 
    public static bool RightControllerBButton => ControllerInputPoller.instance.rightControllerSecondaryButton;
    // Y 
    public static bool LeftControllerYButton => ControllerInputPoller.instance.leftControllerPrimaryButton;
    // X
    public static bool LeftControllerXButton => ControllerInputPoller.instance.leftControllerSecondaryButton;
    // Note: on the htc vive wands both L/R The one button counts for both SecondaryButtons and PrimaryButtons
    // Joystick Stuff
    public static bool RightJoystickClick
    {
        get
        {
            if (SteamVR_Actions.gorillaTag_RightJoystickClick == null)
                return false;

            return SteamVR_Actions.gorillaTag_RightJoystickClick.stateDown;
        }
    }

    public static bool LeftJoystickClick
    {
        get
        {
            if (SteamVR_Actions.gorillaTag_LeftJoystickClick == null)
                return false;

            return SteamVR_Actions.gorillaTag_LeftJoystickClick.stateDown;
        }
    }
}