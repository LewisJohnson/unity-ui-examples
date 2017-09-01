using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ShowSubMenu : MonoBehaviour {

    [SerializeField]
    private Transform _submenuGameObject;

    private bool menuOpen;

    // Use this for initialization
    void Start() {
        if (transform.Find("Submenu")) {
            _submenuGameObject = transform.Find("Submenu");

        } else {
            gameObject.GetComponent<ShowSubMenu>().enabled = false;
        }
    }

    public void OnMouseEnter() {
        if (!gameObject.GetComponent<ShowSubMenu>().enabled)
            return;
        _submenuGameObject.gameObject.SetActive(true);
    }

    public void OnMouseExit() {
        if (!gameObject.GetComponent<ShowSubMenu>().enabled || menuOpen)
            return;
        _submenuGameObject.gameObject.SetActive(false);
    }

    public void ToggleChange() {
        if (this.GetComponent<Toggle>().isOn)
        {
            menuOpen = true;
            _submenuGameObject.GetComponent<CanvasGroup>().alpha = 1f;
        }
        else
        {
            menuOpen = false;
            _submenuGameObject.gameObject.SetActive(false);
            _submenuGameObject.GetComponent<CanvasGroup>().alpha = 0.3f;
        }
        
    }
}
