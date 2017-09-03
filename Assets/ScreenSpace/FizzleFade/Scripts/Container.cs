using UnityEngine;

namespace Assets.ScreenSpace.FizzleFade.Scripts {
    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    public class Container : MonoBehaviour {

        public float Height {
            get { return GetComponent<RectTransform>().sizeDelta.y; }
        }

        public float Width {
            get { return GetComponent<RectTransform>().sizeDelta.x; }
        }

        private void Update() {
            GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        }
    }
}
