using UnityEngine;

[AddComponentMenu("Scripts/Building/BuildPanelButtonHandler")]
public class BuildPanelButtonHandler : MonoBehaviour {

    private Transform _childDescription;
    private Transform _childActionPanel;
    public void Start() {
        _childDescription = transform.Find("Description");
        _childActionPanel = transform.Find("ActionPanel");

        if (_childDescription == null) {
            Debug.LogError(string.Format("The build panel button \"{0}\" does not have a description.", this.name));
        }

        if (_childActionPanel == null) {
            Debug.LogError(string.Format("The build panel button \"{0}\" does not have an action panel.", this.name));
        }
    }

    public void OnPointerEnter() {
        if (!_childActionPanel.gameObject.activeSelf) {
            _childDescription.gameObject.SetActive(true);
        }
    }

    public void OnPointerLeave() {
        _childDescription.gameObject.SetActive(false);
    }

    public void OnPointerClick() {
        _childDescription.gameObject.SetActive(false);

        if (_childActionPanel.gameObject.activeSelf) {
            _childActionPanel.gameObject.SetActive(false);
        } else {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("ActionPanel")) {
                go.SetActive(false);
            }
            _childActionPanel.gameObject.SetActive(true);
        }
    }
}
