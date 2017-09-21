using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[ExecuteInEditMode]
public class BindTextToAnotherTextElement : MonoBehaviour {

    public Text TargetText;

	void Update () {
        this.GetComponent<Text>().text = TargetText.text;
	}
}
