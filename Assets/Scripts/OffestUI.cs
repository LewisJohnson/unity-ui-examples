using System;
using UnityEngine;

namespace Assets.Scripts {
    public class OffestUI : MonoBehaviour {

        public float XClampMax = 50;
        public float YClampMax = 50;

        [Range(5, 50)]
        public int BlackbarPercentage = 20;

        [Range(0, 100)]
        public int Sensitivity = 50;

        public bool InvertOffset = true;
        public bool ControllerOffset;

        public void Start() {
            RefreshUI();
        }

        private void RefreshUI() {
            int screenW = Screen.width;
            int screenH = Screen.height;
            double blackbarHeight = (((double)BlackbarPercentage / 100) * screenH);
            int blackbarWidth = Mathf.RoundToInt(GetComponent<RectTransform>().sizeDelta.x) + (Sensitivity * 2);

            GetComponent<RectTransform>().sizeDelta = new Vector2(screenW, screenH);

            //Can be replaced by transform.find("TopPanel") if you want to move the children.
            RectTransform topPanelRectTransform = transform.GetChild(0).GetComponent<RectTransform>();

            //Can be replaced by transform.find("BottomPanel") if you want to move the children.
            RectTransform bottomPanelRectTransform = transform.GetChild(1).GetComponent<RectTransform>();

            topPanelRectTransform.sizeDelta = new Vector2(blackbarWidth, (int)blackbarHeight);
            topPanelRectTransform.anchoredPosition = new Vector3(0, (int)-(blackbarHeight / 2), 0);

            bottomPanelRectTransform.sizeDelta = new Vector2(blackbarWidth, (int)blackbarHeight);
            bottomPanelRectTransform.anchoredPosition = new Vector3(0, (int)(blackbarHeight / 2), 0);
        }

        public void FixedUpdate() {

            RefreshUI();

            if (Input.GetJoystickNames().Length >= 1 && ControllerOffset) {
                var horz = Input.GetAxis("Horizontal");
                var vert = Input.GetAxis("Vertical");
                var controllerDir = new Vector3(horz, vert, 0);

                if (InvertOffset) {
                    controllerDir = -controllerDir;
                }

                controllerDir.x *= Sensitivity;
                controllerDir.y *= Sensitivity;

                this.GetComponent<RectTransform>().localPosition = controllerDir;

            } else {
                var mouseDir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;

                if (InvertOffset) {
                    mouseDir = -mouseDir;
                }
                mouseDir.x *= Sensitivity;
                mouseDir.y *= Sensitivity;

                mouseDir.x = Mathf.Clamp(mouseDir.x, -XClampMax, XClampMax);
                mouseDir.y = Mathf.Clamp(mouseDir.y, -YClampMax, YClampMax);

                this.GetComponent<RectTransform>().localPosition = mouseDir;
            }

        }
    }
}
