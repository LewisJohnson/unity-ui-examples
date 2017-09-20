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
using Assets.Common.Scripts;

namespace Assets.ScreenSpace.FizzleFade.Scripts {
    public class FizzleFade : MonoBehaviour {

        public Texture2D FizzleTexture2D;
        public Color PixelColour;
        private bool isFilled;
        private int containerWidth;
        private int containerHeight;
        private IEnumerable<IList<int>> xChunks;
        private IEnumerable<IList<int>> yChunks;

        public void Start() {
            // Get container info
            containerWidth = ExtentionMethods.RoundToTenth((int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.x));
            containerHeight = ExtentionMethods.RoundToTenth((int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.y));
            FizzleTexture2D = new Texture2D(containerWidth, containerHeight, TextureFormat.ARGB32, false);

            // Set texture to container
            GetComponent<Image>().material.mainTexture = FizzleTexture2D;

            // Fill texture with transparent pixels
            var fillColorArray = FizzleTexture2D.GetPixels();
            for (var i = 0; i < fillColorArray.Length; ++i) {
                fillColorArray[i] = new Color(0, 0, 0, 0);
            }

            FizzleTexture2D.SetPixels(fillColorArray);
            FizzleTexture2D.Apply();

            // Split container into 16x16 grid
            int xChunkSize = containerWidth / 16;
            int yChunkSize = containerHeight / 16;

            var _xChunks = new List<int>();
            var _yChunks = new List<int>();

            for (int i = 0; i < containerWidth; i++) {
                _xChunks.Add(i);
            }

            for (int i = 0; i < containerHeight; i++) {
                _yChunks.Add(i);
            }

            xChunks = _xChunks.Chunks(xChunkSize);
            yChunks = _yChunks.Chunks(xChunkSize);

        }

        private int x = 0;
        private int y = 0;
        private void SetNextPixel() {
            if (x != containerWidth && y != containerHeight) {
                for (int i = x; i <= x + 10; i++) {
                    FizzleTexture2D.SetPixel(i, y, PixelColour);
                }

                x = x + 10;
                y = y + 10;
                FizzleTexture2D.Apply();
            } else {
                isFilled = true;
            }
        }

        public void Fizzle(Color pixelColour) {
            //GetComponent<Image>().material.mainTexture = FizzleTexture2D;

            //for (int x = 0; x <= width; x++) {
            //    for (int y = 0; y <= height; y++) {
            //        StartCoroutine(WaitAndPrint(x, y, pixelColour, 1));
            //        FizzleTexture2D.SetPixel(x, y, pixelColour);
            //    }
            //}
        }


        private IEnumerator WaitAndPrint(int x, int y, Color pixelColour, float waitTime) {
            FizzleTexture2D.SetPixel(x, y, pixelColour);
            yield return new WaitForSeconds(waitTime);
        }

        public void Update() {
            if (!isFilled) {
                SetNextPixel();
            }
        }

        public static void Shuffle<T>(IList<T> list) {
            int n = list.Count;
            var rng = new System.Random();
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


