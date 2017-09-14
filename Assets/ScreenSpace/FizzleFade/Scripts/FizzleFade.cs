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

    public class FizzleFade : MonoBehaviour {

        public Texture2D FizzleTexture2D;
        private bool _isFilled;

        public void Start() {
            Fizzle(new Color(255, 0, 0, 255));
        }

        public void Fizzle(Color pixelColour) {

            int width = (int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.x);
            int height = (int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.y);
            FizzleTexture2D = new Texture2D(height, width, TextureFormat.ARGB32, false);

            var fillColorArray = FizzleTexture2D.GetPixels();

            for (var i = 0; i < fillColorArray.Length; ++i) {
                fillColorArray[i] = new Color(0, 0, 0, 0);
            }

            FizzleTexture2D.SetPixels(fillColorArray);
            FizzleTexture2D.Apply();
            GetComponent<Image>().material.mainTexture = FizzleTexture2D;

            for (int x = 0; x <= width; x++) {
                for (int y = 0; y <= height; y++) {
                    StartCoroutine(WaitAndPrint(x, y, pixelColour, 1));
                    FizzleTexture2D.SetPixel(x, y, pixelColour);
                }
            }

            
        }


        private IEnumerator WaitAndPrint(int x, int y, Color pixelColour, float waitTime) {
            
            FizzleTexture2D.SetPixel(x, y, pixelColour);
            
            yield return new WaitForSeconds(waitTime);

        }

        public void FixedUpdate()
        {
            if (!_isFilled)
            {
                FizzleTexture2D.Apply();
            }
            
        }

        public static void Shuffle<T>(IList<T> list) {
            int n = list.Count;
            var rng = new Random();
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


