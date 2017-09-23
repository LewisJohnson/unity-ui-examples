using System;
using Assets.Common.StandardAssets.CrossPlatformInput.Scripts.PlatformSpecific;
using UnityEngine;

namespace Assets.Common.StandardAssets.CrossPlatformInput.Scripts {
    [AddComponentMenu("Scripts/Standard Assets/CrossPlatformInput/CrossPlatformInputManager")]
    public static class CrossPlatformInputManager {
        public enum ActiveInputMethod {
            Hardware,
            Touch
        }

        private static VirtualInput _activeInput;
        private static readonly VirtualInput STouchInput;
        private static readonly VirtualInput SHardwareInput;


        static CrossPlatformInputManager() {
            STouchInput = new MobileInput();
            SHardwareInput = new StandaloneInput();
#if MOBILE_INPUT
            activeInput = s_TouchInput;
#else
            _activeInput = SHardwareInput;
#endif
        }

        public static void SwitchActiveInputMethod(ActiveInputMethod activeInputMethod) {
            switch (activeInputMethod) {
                case ActiveInputMethod.Hardware:
                _activeInput = SHardwareInput;
                break;

                case ActiveInputMethod.Touch:
                _activeInput = STouchInput;
                break;
                default:
                throw new ArgumentOutOfRangeException("activeInputMethod", activeInputMethod, null);
            }
        }

        public static bool AxisExists(string name) {
            return _activeInput.AxisExists(name);
        }

        public static bool ButtonExists(string name) {
            return _activeInput.ButtonExists(name);
        }

        public static void RegisterVirtualAxis(VirtualAxis axis) {
            _activeInput.RegisterVirtualAxis(axis);
        }


        public static void RegisterVirtualButton(VirtualButton button) {
            _activeInput.RegisterVirtualButton(button);
        }


        public static void UnRegisterVirtualAxis(string name) {
            if (name == null) {
                throw new ArgumentNullException("name");
            }

            _activeInput.UnRegisterVirtualAxis(name);
        }


        public static void UnRegisterVirtualButton(string name) {
            _activeInput.UnRegisterVirtualButton(name);
        }


        // returns a reference to a named virtual axis if it exists otherwise null
        public static VirtualAxis VirtualAxisReference(string name) {
            return _activeInput.VirtualAxisReference(name);
        }


        // returns the platform appropriate axis for the given name
        public static float GetAxis(string name) {
            return GetAxis(name, false);
        }


        public static float GetAxisRaw(string name) {
            return GetAxis(name, true);
        }


        // private function handles both types of axis (raw and not raw)
        private static float GetAxis(string name, bool raw) {
            return _activeInput.GetAxis(name, raw);
        }


        // -- Button handling --
        public static bool GetButton(string name) {
            return _activeInput.GetButton(name);
        }


        public static bool GetButtonDown(string name) {
            return _activeInput.GetButtonDown(name);
        }


        public static bool GetButtonUp(string name) {
            return _activeInput.GetButtonUp(name);
        }


        public static void SetButtonDown(string name) {
            _activeInput.SetButtonDown(name);
        }


        public static void SetButtonUp(string name) {
            _activeInput.SetButtonUp(name);
        }


        public static void SetAxisPositive(string name) {
            _activeInput.SetAxisPositive(name);
        }


        public static void SetAxisNegative(string name) {
            _activeInput.SetAxisNegative(name);
        }


        public static void SetAxisZero(string name) {
            _activeInput.SetAxisZero(name);
        }


        public static void SetAxis(string name, float value) {
            _activeInput.SetAxis(name, value);
        }


        public static Vector3 MousePosition {
            get { return _activeInput.MousePosition(); }
        }


        public static void SetVirtualMousePositionX(float f) {
            _activeInput.SetVirtualMousePositionX(f);
        }


        public static void SetVirtualMousePositionY(float f) {
            _activeInput.SetVirtualMousePositionY(f);
        }


        public static void SetVirtualMousePositionZ(float f) {
            _activeInput.SetVirtualMousePositionZ(f);
        }


        // virtual axis and button classes - applies to mobile input
        // Can be mapped to touch joysticks, tilt, gyro, etc, depending on desired implementation.
        // Could also be implemented by other input devices - kinect, electronic sensors, etc
        public class VirtualAxis {
            public string Name { get; private set; }
            private float _mValue;
            public bool MatchWithInputManager { get; private set; }


            public VirtualAxis(string name)
                : this(name, true) {
            }


            public VirtualAxis(string name, bool matchToInputSettings) {
                this.Name = name;
                MatchWithInputManager = matchToInputSettings;
            }

            // removes an axes from the cross platform input system
            public void Remove() {
                UnRegisterVirtualAxis(Name);
            }

            // a controller gameobject (eg. a virtual thumbstick) should update this class
            public void Update(float value) {
                _mValue = value;
            }

            public float GetValue {
                get { return _mValue; }
            }


            public float GetValueRaw {
                get { return _mValue; }
            }
        }

        // a controller gameobject (eg. a virtual GUI button) should call the
        // 'pressed' function of this class. Other objects can then read the
        // Get/Down/Up state of this button.
        public class VirtualButton {
            public string Name { get; private set; }
            public bool MatchWithInputManager { get; private set; }

            private int _mLastPressedFrame = -5;
            private int _mReleasedFrame = -5;
            private bool _mPressed;


            public VirtualButton(string name)
                : this(name, true) {
            }


            public VirtualButton(string name, bool matchToInputSettings) {
                this.Name = name;
                MatchWithInputManager = matchToInputSettings;
            }


            // A controller gameobject should call this function when the button is pressed down
            public void Pressed() {
                if (_mPressed) {
                    return;
                }

                _mPressed = true;
                _mLastPressedFrame = Time.frameCount;
            }


            // A controller gameobject should call this function when the button is released
            public void Released() {
                _mPressed = false;
                _mReleasedFrame = Time.frameCount;
            }


            // the controller gameobject should call Remove when the button is destroyed or disabled
            public void Remove() {
                UnRegisterVirtualButton(Name);
            }


            // these are the states of the button which can be read via the cross platform input system
            public bool GetButton {
                get { return _mPressed; }
            }


            public bool GetButtonDown {
                get {
                    return _mLastPressedFrame - Time.frameCount == -1;
                }
            }

            public bool GetButtonUp {
                get {
                    return _mReleasedFrame == Time.frameCount - 1;
                }
            }
        }
    }
}
