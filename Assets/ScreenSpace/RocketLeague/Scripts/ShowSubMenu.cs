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
using UnityEngine.UI;

namespace Assets.ScreenSpace.RocketLeague.Scripts
{
    [RequireComponent(typeof(Toggle))]
    [AddComponentMenu("Scripts/Rocket League/Show Sub Menu")]
    public class ShowSubMenu : MonoBehaviour {

        [SerializeField]
        private Transform _submenuGameTransform;

        [SerializeField]
        private Transform _backButtonTransform;

        private bool _menuOpen;

        // Use this for initialization
        void Start() {
            if (_submenuGameTransform == null) {
                Debug.LogWarning("Submenu has not been assigned.");
            }
        }

        public void OnMouseEnter() {
            if (!gameObject.GetComponent<ShowSubMenu>().enabled || _submenuGameTransform == null)
                return;
            _submenuGameTransform.gameObject.SetActive(true);
        }

        public void OnMouseExit() {
            if (!gameObject.GetComponent<ShowSubMenu>().enabled || _menuOpen || _submenuGameTransform == null)
                return;
            _submenuGameTransform.gameObject.SetActive(false);
        }

        public void ToggleChange() {
            if (GetComponent<Toggle>().isOn) {
                _menuOpen = true;

                _backButtonTransform.gameObject.SetActive(true);
                _backButtonTransform.GetComponent<BackButton>().SubMenuGameObject = _submenuGameTransform.gameObject;

                transform.parent.GetComponent<Animation>().Play();
                _submenuGameTransform.GetComponent<Animation>().Play();
            } else {
                _menuOpen = false;

                _backButtonTransform.gameObject.SetActive(false);
                _backButtonTransform.GetComponent<BackButton>().SubMenuGameObject = null;

                _submenuGameTransform.gameObject.SetActive(false);
                _submenuGameTransform.GetComponent<CanvasGroup>().alpha = 0.3f;
            }

        }
    }
}
