using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Common.Scripts {

    [AddComponentMenu("Scripts/Common/Vertical Text")]
    [RequireComponent(typeof(Text))]
    [ExecuteInEditMode]
    public class VerticalText : MonoBehaviour {

        // Use this for initialization
        private void OnEnable() {
            string input = GetComponent<Text>().text;
            StringBuilder sb = new StringBuilder(input.Length * 2);
            foreach (char chr in input) {
                sb.Append(chr).Append("\n");
            }

            GetComponent<Text>().text = sb.ToString();
        }
    }

}