using UnityEngine;

namespace Assets.Common.StandardAssets.CrossPlatformInput.Scripts
{
    [AddComponentMenu("Scripts/Standard Assets/CrossPlatformInput/InputAxisScrollbar")]
    public class InputAxisScrollbar : MonoBehaviour
    {
        public string axis;

	    public void HandleInput(float value)
        {
            CrossPlatformInputManager.SetAxis(axis, (value*2f) - 1f);
        }
    }
}
