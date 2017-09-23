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

namespace Assets.ScreenSpace.PrisionArchitect.Scripts {

    [AddComponentMenu("Scripts/Building/BuildPanelButtonHandler")]
    public class BuildPanelButtonHandler : MonoBehaviour {
        private Transform childActionPanel;
        private Transform childDescription;

        public void OnPointerClick() {
            childDescription.gameObject.SetActive(false);

            if (childActionPanel.gameObject.activeSelf) {
                childActionPanel.gameObject.SetActive(false);
            } else {
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("ActionPanel")) {
                    go.SetActive(false);
                }

                childActionPanel.gameObject.SetActive(true);
            }
        }

        public void OnPointerEnter() {
            if (!childActionPanel.gameObject.activeSelf) {
                childDescription.gameObject.SetActive(true);
            }
        }

        public void OnPointerLeave() {
            childDescription.gameObject.SetActive(false);
        }

        public void Start() {
            childDescription = transform.Find("Description");
            childActionPanel = transform.Find("ActionPanel");

            if (childDescription == null) {
                Debug.LogError(string.Format("The build panel button \"{0}\" does not have a description.", name));
            }

            if (childActionPanel == null) {
                Debug.LogError(string.Format("The build panel button \"{0}\" does not have an action panel.", name));
            }
        }
    }

}