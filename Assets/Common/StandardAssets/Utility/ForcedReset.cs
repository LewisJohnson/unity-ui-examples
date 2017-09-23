using Assets.Common.StandardAssets.CrossPlatformInput.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Common.StandardAssets.Utility
{
    [RequireComponent(typeof (GUITexture))]
    [AddComponentMenu("Scripts/Standard Assets/Utility/Forced Reset")]
    public class ForcedReset : MonoBehaviour
    {
        private void Update()
        {
            // if we have forced a reset ...
            if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
            {
                // ... reload the scene
                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
            }
        }
    }
}
