using SlimDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlebot_Control_Hub
{
    public struct DPad
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;
    };

    public struct Thumbstick
    {
        public bool Click;
        public double X;
        public double Y;
    };

    public struct Thumbsticks
    {
        public Thumbstick Left;
        public Thumbstick Right;
    };

    public class Gamepad
    {
        // XInput controller instance
        private Controller controller;

        // Controller data members
        public bool A;
        public bool B;
        public bool X;
        public bool Y;
        public bool Start;
        public bool Back;
        public DPad DPad;
        public bool LeftBumper;
        public bool RightBumper;
        public double LeftTrigger;
        public double RightTrigger;
        public Thumbsticks Thumbsticks;

        public Gamepad(int player_num)
        {
            if (player_num < 0 || player_num > 4)
            {
                throw new ArgumentException();
            }

            // Get the user index
            SlimDX.XInput.UserIndex userIndex = SlimDX.XInput.UserIndex.Any;
            switch (player_num)
            {
                case 1:
                    userIndex = SlimDX.XInput.UserIndex.One;
                    break;
                case 2:
                    userIndex = SlimDX.XInput.UserIndex.Two;
                    break;
                case 3:
                    userIndex = SlimDX.XInput.UserIndex.Three;
                    break;
                case 4:
                    userIndex = SlimDX.XInput.UserIndex.Four;
                    break;
            }

            controller = new Controller(userIndex);
        }

        // Returns a string representing which buttons have been pressed
        public String Buttons()
        {
            StringBuilder sb = new StringBuilder();
            if (A)
            {
                sb.Append("A, ");
            }

            ;
            if (B)
            {
                sb.Append("B, ");
            }

            ;
            if (X)
            {
                sb.Append("X, ");
            }

            ;
            if (Y)
            {
                sb.Append("Y, ");
            }

            ;
            if (Start)
            {
                sb.Append("Start, ");
            }

            ;
            if (Back)
            {
                sb.Append("Back, ");
            }

            ;
            if (LeftBumper)
            {
                sb.Append("LBumper, ");
            }

            ;
            if (RightBumper)
            {
                sb.Append("RBumper, ");
            }

            ;
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }

            return sb.ToString();
        }

        public void LoadState()
        {
            // Get the slimdx game pad state
            SlimDX.XInput.Gamepad state = controller.GetState().Gamepad;

            // Updated button values
            A = ((state.Buttons & GamepadButtonFlags.A) == GamepadButtonFlags.A);
            B = ((state.Buttons & GamepadButtonFlags.B) == GamepadButtonFlags.B);
            X = ((state.Buttons & GamepadButtonFlags.X) == GamepadButtonFlags.X);
            Y = ((state.Buttons & GamepadButtonFlags.Y) == GamepadButtonFlags.Y);
            Start = ((state.Buttons & GamepadButtonFlags.Start) == GamepadButtonFlags.Start);
            Back = ((state.Buttons & GamepadButtonFlags.Back) == GamepadButtonFlags.Back);
            DPad.Up = ((state.Buttons & GamepadButtonFlags.DPadUp) == GamepadButtonFlags.DPadUp);
            DPad.Down = ((state.Buttons & GamepadButtonFlags.DPadDown) == GamepadButtonFlags.DPadDown);
            DPad.Left = ((state.Buttons & GamepadButtonFlags.DPadLeft) == GamepadButtonFlags.DPadLeft);
            DPad.Right = ((state.Buttons & GamepadButtonFlags.DPadRight) == GamepadButtonFlags.DPadRight);
            LeftBumper = ((state.Buttons & GamepadButtonFlags.LeftShoulder) == GamepadButtonFlags.LeftShoulder);
            RightBumper = ((state.Buttons & GamepadButtonFlags.RightShoulder) == GamepadButtonFlags.RightShoulder);

            // Update trigger values
            if (state.LeftTrigger > SlimDX.XInput.Gamepad.GamepadTriggerThreshold)
            {
                LeftTrigger = state.LeftTrigger / 255.0;
            }
            else
            {
                LeftTrigger = 0;
            }

            if (state.RightTrigger > SlimDX.XInput.Gamepad.GamepadTriggerThreshold)
            {
                RightTrigger = state.RightTrigger / 255.0;
            }
            else
            {
                RightTrigger = 0;
            }

            // Update thumbstick values
            Thumbsticks.Left.Click = ((state.Buttons & GamepadButtonFlags.LeftThumb) == GamepadButtonFlags.LeftThumb);
            Thumbsticks.Right.Click =
                ((state.Buttons & GamepadButtonFlags.RightThumb) == GamepadButtonFlags.RightThumb);

            Vector2 leftStick =
                new Vector2(state.LeftThumbX, state.LeftThumbY).Normalize(
                    SlimDX.XInput.Gamepad.GamepadLeftThumbDeadZone);
            Vector2 rightStick =
                new Vector2(state.RightThumbX, state.RightThumbY).Normalize(SlimDX.XInput.Gamepad
                    .GamepadRightThumbDeadZone);
            Thumbsticks.Left.X = leftStick.X;
            Thumbsticks.Left.Y = leftStick.Y;

            Thumbsticks.Right.X = rightStick.X;
            Thumbsticks.Right.Y = rightStick.Y;
        }
    }
}
