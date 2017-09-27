/* MIT License

Copyright(c) 2017 Lewis Johnson

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

namespace Assets.WorldSpace.Transitions.Scripts {

    [AddComponentMenu("Scripts/Transitions/Fade InOut")]
    [RequireComponent(typeof(Image))]
    [ExecuteInEditMode]
    public class FadeInOut : MonoBehaviour {

        public float TransitionTime = 2f;
        private float timer;
        public float HangTime = 2f;
        public bool Fadein;

        public void Update() {
            if (Fadein) {
                FadeIn();
            }
        }
        // Update is called once per frame
        public void FadeIn() {
            if (timer > TransitionTime) {
                Fadein = false;
                StartCoroutine("FadeOut", HangTime);
            }

            timer += Time.deltaTime;
            var c = this.GetComponent<Image>().color;
            c.a = timer;
            this.GetComponent<Image>().color = c;
        }

        // Use this for initialization
        public void FadeOut() {
            
        }
    }

}