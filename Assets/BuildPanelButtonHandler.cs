using UnityEngine;

public class BuildPanelButtonHandler : MonoBehaviour {

    private Transform _childDescription;

    public void Start () {
        _childDescription = transform.Find("Description");
        if(_childDescription == null){
            Debug.LogError(string.Format("The build panel button \"{0}\" does not have a description.", this.name));
        }
	}

    public void onPointerEnter() {
        _childDescription.gameObject.SetActive(true);
	}

    public void onPointerLeave(){
        _childDescription.gameObject.SetActive(false);
    }
}
