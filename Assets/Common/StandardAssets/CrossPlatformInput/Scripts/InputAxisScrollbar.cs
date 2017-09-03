using UnityEngine;

namespace Assets.Common.StandardAssets.CrossPlatformInput.Scripts
{
    public class InputAxisScrollbar : MonoBehaviour
    {
        public string axis;

	    void Update() { }

	    public void HandleInput(float value)
        {
            CrossPlatformInputManager.SetAxis(axis, (value*2f) - 1f);
        }
    }
}
