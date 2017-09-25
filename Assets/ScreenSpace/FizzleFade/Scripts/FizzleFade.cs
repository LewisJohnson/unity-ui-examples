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
using Assets.Common.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Assets.ScreenSpace.FizzleFade.Scripts {

    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RectTransform))]
    [AddComponentMenu("Scripts/Fizzlefade/FizzleFade")]
    public class FizzleFade : MonoBehaviour {

        public Texture2D FizzleTexture2D;
        public Color PixelColour = new Color(255, 0, 0, 255);
        private readonly int amountOfDivisions = 16;

        private int chunkLoopIndex;
        private IList<int>[,] combinedChunks;
        private int containerHeight;
        private int containerWidth;
        private bool isFilled;
        private IEnumerable<IList<int>> xChunks;
        private IEnumerable<IList<int>> yChunks;

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

        public void FixedUpdate() {
            if (!isFilled) {
                StartCoroutine(WaitAndPrint());
            }
        }

        public void Start() {
            // Get container info
            containerWidth = ((int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.x)).RoundToTenth();
            containerHeight = ((int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.y)).RoundToTenth();
            FizzleTexture2D = new Texture2D(containerWidth, containerHeight, TextureFormat.ARGB32, false);

            // Set texture to container
            GetComponent<Image>().material.mainTexture = FizzleTexture2D;

            // Fill texture with transparent pixels
            Color[] fillColorArray = FizzleTexture2D.GetPixels();
            for (int i = 0; i < fillColorArray.Length; ++i) {
                fillColorArray[i] = new Color(0, 0, 0, 0);
            }

            FizzleTexture2D.SetPixels(fillColorArray);
            FizzleTexture2D.Apply();

            // Split container into 16x16 grid
            int xChunkSize = containerWidth / amountOfDivisions;
            int yChunkSize = containerHeight / 16;

            List<int> _xChunks = new List<int>();
            List<int> _yChunks = new List<int>();

            for (int i = 0; i < containerWidth; i++) {
                _xChunks.Add(i);
            }

            for (int i = 0; i < containerHeight; i++) {
                _yChunks.Add(i);
            }

            xChunks = _xChunks.Chunks(xChunkSize);
            yChunks = _yChunks.Chunks(yChunkSize);

            combinedChunks = new IList<int>[amountOfDivisions, 2];
            int j = 0;
            int k = 0;
            foreach (IList<int> item in xChunks) {
                combinedChunks[j, 0] = item;
                j++;
            }

            foreach (IList<int> item in yChunks) {
                combinedChunks[k, 1] = item;
                k++;
            }
        }

        private void SetNextPixel() {
            // ReSharper disable once UnusedVariable
            int[,] pixels = new int[combinedChunks[chunkLoopIndex, 0].Count, combinedChunks[chunkLoopIndex, 1].Count];

            for (int i = 0; i < combinedChunks[chunkLoopIndex, 0].Count; i++) {
                for (int j = 0; j < combinedChunks[chunkLoopIndex, 1].Count; j++) {
                    FizzleTexture2D.SetPixel(
                        combinedChunks[chunkLoopIndex, 0][i],
                        combinedChunks[chunkLoopIndex, 1][j],
                        PixelColour);
                }
            }

            FizzleTexture2D.Apply();
            if (chunkLoopIndex == amountOfDivisions) {
                isFilled = true;
            }

            chunkLoopIndex++;
        }

        private IEnumerator WaitAndPrint() {
            SetNextPixel();
            yield return new WaitForSeconds(4);
        }
    }

}