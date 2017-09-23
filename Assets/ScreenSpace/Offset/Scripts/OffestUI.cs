/* 
MIT License

Copyright (c) 2017 Lewis Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using UnityEngine;

namespace Assets.ScreenSpace.Offset.Scripts {

    [AddComponentMenu("Scripts/Offset/OffsetUI")]
    public class OffestUI : MonoBehaviour {

        [Range(5, 50)]
        public int BlackbarPercentage = 20;
        public bool ControllerOffset;
        public bool InvertOffset = true;

        [Range(0, 100)]
        public int Sensitivity = 50;
        public float XClampMax = 50;
        public float YClampMax = 50;

        public void FixedUpdate() {
            RefreshUI();

            if (Input.GetJoystickNames().Length >= 1 && ControllerOffset) {
                float horz = Input.GetAxis("Horizontal");
                float vert = Input.GetAxis("Vertical");
                Vector3 controllerDir = new Vector3(horz, vert, 0);

                if (InvertOffset) {
                    controllerDir = -controllerDir;
                }

                controllerDir.x *= Sensitivity;
                controllerDir.y *= Sensitivity;

                GetComponent<RectTransform>().localPosition = controllerDir;
            } else {
                Vector3 mouseDir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;

                if (InvertOffset) {
                    mouseDir = -mouseDir;
                }

                mouseDir.x *= Sensitivity;
                mouseDir.y *= Sensitivity;
                mouseDir.x = Mathf.Clamp(mouseDir.x, -XClampMax, XClampMax);
                mouseDir.y = Mathf.Clamp(mouseDir.y, -YClampMax, YClampMax);
                GetComponent<RectTransform>().localPosition = mouseDir;
            }
        }

        public void Start() {
            RefreshUI();
        }

        private void RefreshUI() {
            int screenW = Screen.width;
            int screenH = Screen.height;
            double blackbarHeight = (double)BlackbarPercentage / 100 * screenH;
            int blackbarWidth = Mathf.RoundToInt(GetComponent<RectTransform>().sizeDelta.x) + Sensitivity * 2;
            GetComponent<RectTransform>().sizeDelta = new Vector2(screenW, screenH);

            // Can be replaced by transform.find("TopPanel") if you want to move the children.
            RectTransform topPanelRectTransform = transform.GetChild(0).GetComponent<RectTransform>();

            // Can be replaced by transform.find("BottomPanel") if you want to move the children.
            RectTransform bottomPanelRectTransform = transform.GetChild(1).GetComponent<RectTransform>();

            topPanelRectTransform.sizeDelta = new Vector2(blackbarWidth, (int)blackbarHeight);
            topPanelRectTransform.anchoredPosition = new Vector3(0, (int)-(blackbarHeight / 2), 0);

            bottomPanelRectTransform.sizeDelta = new Vector2(blackbarWidth, (int)blackbarHeight);
            bottomPanelRectTransform.anchoredPosition = new Vector3(0, (int)(blackbarHeight / 2), 0);
        }

    }

}