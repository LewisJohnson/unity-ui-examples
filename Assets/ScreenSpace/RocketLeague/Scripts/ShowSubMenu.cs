using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScreenSpace.RocketLeague.Scripts
{
    [RequireComponent(typeof(Toggle))]
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
