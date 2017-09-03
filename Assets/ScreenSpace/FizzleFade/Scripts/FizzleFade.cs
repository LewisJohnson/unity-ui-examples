using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


namespace Assets.ScreenSpace.FizzleFade.Scripts {

    public class FizzleFade : MonoBehaviour {

        public GameObject Image;
        public void Start() {
            Fizzle(new Color(255, 0, 0, 255));
        }

        public void StartFizzleFade(Color pixelColour) {

            int width = (int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.x);
            int height = (int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.y);

            var xAxisFilled = new List<int>();
            var yAxisFilled = new List<int>();
            var rand = new Random();

            while (xAxisFilled.Count != width && yAxisFilled.Count != height) {

                int x = rand.Next(0, width);
                if (xAxisFilled.Contains(x))
                    continue;

                int y = rand.Next(0, height);
                if (yAxisFilled.Contains(y))
                    continue;

                var img = Instantiate(Image, transform);
                img.GetComponent<Image>().color = pixelColour;
                img.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

                xAxisFilled.Add(x);
                yAxisFilled.Add(y);

            }
        }

        public void StartFizzleFade2(Color pixelColour) {

            int width = (int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.x) / 100;
            int height = (int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.y) / 100;

            var xAxisFilled = new List<int>(width);
            var xrandom = new Random();
            xAxisFilled.Add(0);
            for (var i = 1; i < width; i++) {
                var swap = xrandom.Next(i - 1);
                xAxisFilled.Add(xAxisFilled[swap]);
                xAxisFilled[swap] = i;
            }

            var yAxisFilled = new List<int>(height);
            var random = new Random();
            yAxisFilled.Add(0);
            for (var i = 1; i < height; i++) {
                var swap = random.Next(i - 1);
                yAxisFilled.Add(yAxisFilled[swap]);
                yAxisFilled[swap] = i;
            }

            for (int x = 0; x < xAxisFilled.Count; x++) {
                for (int y = 0; y < yAxisFilled.Count; y++) {
                    var img = Instantiate(Image, transform);
                    img.GetComponent<Image>().color = pixelColour;
                    img.GetComponent<RectTransform>().anchoredPosition = new Vector2(xAxisFilled[x], yAxisFilled[y]);
                }

            }
        }

        public void Fizzle(Color pixelColour) {

            int width = (int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.x);
            int height = (int)Math.Ceiling(GetComponent<RectTransform>().sizeDelta.y);

            var xList = new List<int>();
            var yList = new List<int>();
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
                    var img = Instantiate(Image, transform);
                    img.GetComponent<Image>().color = pixelColour;
                    img.GetComponent<RectTransform>().anchoredPosition = new Vector2(xList[x], -yList[y]);
                    img.SetActive(false);
                    StartCoroutine(WaitAndPrint(img, (x * y) / 1000));
                }

            }

            //var child = new List<int>(transform.childCount);
            //var xrandom = new Random();
            //child.Add(0);
            //for (var i = 1; i < width; i++) {
            //    var swap = xrandom.Next(i - 1);
            //    child.Add(child[swap]);
            //    child[swap] = i;
            //}

            //for (int i = 0; i < child.Count; i++) {
            //    StartCoroutine(WaitAndPrint(child[i], 1));
            //}

        }


        private IEnumerator WaitAndPrint(GameObject k, float waitTime) {
            yield return new WaitForSeconds(waitTime);
            k.SetActive(true);
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


