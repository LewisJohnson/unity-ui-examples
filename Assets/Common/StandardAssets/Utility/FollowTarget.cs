using UnityEngine;

namespace Assets.Common.StandardAssets.Utility {
    [AddComponentMenu("Scripts/Standard Assets/Utility/Follow Target")]
    public class FollowTarget : MonoBehaviour {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 7.5f, 0f);


        private void LateUpdate() {
            transform.position = target.position + offset;
        }
    }
}
