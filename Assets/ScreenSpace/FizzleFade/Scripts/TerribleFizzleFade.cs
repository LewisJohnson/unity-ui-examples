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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Assets.ScreenSpace.FizzleFade.Scripts {

    /// <summary>
    /// Please don't use this. It works but is a hot mess.
    /// </summary>
    [AddComponentMenu("Scripts/Fizzlefade/TerribleFizzleFade")]
    public class TerribleFizzleFade : MonoBehaviour {

        public GameObject Image;

        public void Start() {
            Fizzle(new Color(255, 0, 0, 255));
        }

        public void Fizzle(Color pixelColour) {
            int width = (int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.x);
            int height = (int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.y);

            List<int> xList = new List<int>();
            List<int> yList = new List<int>();
            for (int i = 0; i <= ((int)Math.Round(width / 10.0)) * 10; i += 10) {
                xList.Add(i);
            }

            for (int i = 0; i <= ((int)Math.Round(height / 10.0)) * 10; i += 10) {
                yList.Add(i);
            }

            Shuffle(xList);
            Shuffle(yList);

            for (int x = 0; x < xList.Count; x++) {
                for (int y = 0; y < yList.Count; y++) {
                    GameObject img = Instantiate(Image, transform);
                    img.GetComponent<Image>().color = pixelColour;
                    img.GetComponent<RectTransform>().anchoredPosition = new Vector2(xList[x], -yList[y]);
                    img.SetActive(false);
                    StartCoroutine(WaitAndPrint(img, (x * y) / 1000));
                }
            }
        }

        private IEnumerator WaitAndPrint(GameObject k, float waitTime) {
            yield return new WaitForSeconds(waitTime);
            k.SetActive(true);
        }

        public static void Shuffle<T>(IList<T> list) {
            int n = list.Count;
            Random rng = new Random();
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }

}