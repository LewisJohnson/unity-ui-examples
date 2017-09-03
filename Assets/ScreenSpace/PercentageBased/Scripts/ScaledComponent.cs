using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScreenSpace.PercentageBased.Scripts {

    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    public class ScaledComponent : MonoBehaviour {

        public RectTransform Container;
        public Position ComponentPosition;
        public Scale ComponentScale;

#if UNITY_EDITOR
        public GameObject DebugText;
#endif
        public void Start() {
            RefreshUI();
        }

        private void RefreshUI() {

            float width = (ComponentScale.Width / 100) * Container.GetComponent<Container>().Width;
            float height = (ComponentScale.Height / 100) * Container.GetComponent<Container>().Height;

            float top = (ComponentPosition.Top / 100) * Container.GetComponent<Container>().Height;
            float left = (ComponentPosition.Left / 100) * Container.GetComponent<Container>().Width;

            GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            GetComponent<RectTransform>().anchoredPosition = new Vector3(left, top, 0);

#if UNITY_EDITOR
            DebugText.GetComponent<Text>().text = PrettyPrint();
#endif
        }

        private string PrettyPrint() {
            return string.Format("Top: {0}%\nLeft: {1}%\nWidth: {2}%\nHeight: {3}%", ComponentPosition.Top, ComponentPosition.Left, ComponentScale.Width, ComponentScale.Height);
        }

        public void Update() {
            RefreshUI();
        }
    }

    [Serializable]
    public class Position {
        public float Left;
        public float Top;
    }

    [Serializable]
    public class Scale {
        public float Width;
        public float Height;
    }
}
