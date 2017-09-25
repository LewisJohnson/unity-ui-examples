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
using System.Globalization;
using Assets.ScreenSpace.PercentageBased.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.ScreenSpace.BarChart.Scripts {

    [AddComponentMenu("Scripts/Bar Chart/Bar Chart Manager")]
    [RequireComponent(typeof(RectTransform))]

    // [ExecuteInEditMode]
    public class BarChartManager : MonoBehaviour {
        public GameObject BarChartComponentGameObject;
        public BarChartColourConfig ChartColourConfig;

        [SerializeField] private Slider.Direction barDirection;
        [Range(0, 100)] [SerializeField] private float heightPercentage;
        [Range(0, 100)] [SerializeField] private float leftPaddingPercentage;
        [Range(0, 100)] [SerializeField] private float spacePercentage;
        [Range(0, 100)] [SerializeField] private float widthPercentage;
        [SerializeField] private bool wholeNumbers;

        public Slider.Direction BarDirection {
            get { return barDirection; }
            set {
                barDirection = value;
                UpdateVisuals();
            }
        }

        public float HeightPercentage {
            get { return heightPercentage; }
            set {
                heightPercentage = value;
                UpdateVisuals();
            }
        }

        public float LeftPaddingPercentage {
            get { return leftPaddingPercentage; }
            set {
                leftPaddingPercentage = value;
                UpdateVisuals();
            }
        }

        public float SpacePercentage {
            get { return spacePercentage; }
            set {
                spacePercentage = value;
                UpdateVisuals();
            }
        }

        public bool WholeNumbers {
            get { return wholeNumbers; }
            set {
                wholeNumbers = value;
                UpdateVisuals();
            }
        }

        public float WidthPercentage {
            get { return widthPercentage; }
            set {
                widthPercentage = value;
                UpdateVisuals();
            }
        }

        public void AddNewBar(float min, float max, float value, bool showValue) {
            if (BarChartComponentGameObject.GetComponent<Slider>() == null
                || BarChartComponentGameObject.GetComponent<ScaledComponent>() == null
                || BarChartComponentGameObject.GetComponent<BarChartComponent>() == null) {
                return;
            }

            if (min > max) {
                return;
            }

            if (value < min || value > max) {
                return;
            }

            GameObject bcc = Instantiate(BarChartComponentGameObject, transform);
            Slider bccSlider = bcc.GetComponent<Slider>();
            BarChartComponent bccScript = bcc.GetComponent<BarChartComponent>();

            bccSlider.minValue = min;
            bccSlider.maxValue = max;
            bccSlider.value = value;
            bccScript.ShowValueText = showValue;

            UpdateVisuals();
        }

        public void UpdateVisuals() {
            int barChildren = 0;
            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).tag != "Bar") {
                    continue;
                }
                
                if (transform.GetChild(i).GetComponent<Slider>() == null
                    || transform.GetChild(i).GetComponent<ScaledComponent>() == null
                    || transform.GetChild(i).GetComponent<BarChartComponent>() == null) {
                    Debug.LogWarningFormat(
                        transform,
                        "Manager contains child not fit for purpose.",
                        transform.GetChild(i));
                    return;
                }

                // locals
                Slider childSlider = transform.GetChild(i).GetComponent<Slider>();
                ScaledComponent childScaledComponent = transform.GetChild(i).GetComponent<ScaledComponent>();
                BarChartComponent childBcc = transform.GetChild(i).GetComponent<BarChartComponent>();

                // Slider
                childSlider.direction = barDirection;
                childSlider.wholeNumbers = WholeNumbers;

                // BCC
                childBcc.ValueText.text = childSlider.value.ToString(CultureInfo.CurrentCulture);

                // Scaled comp
                if (barChildren == 0) {
                    childScaledComponent.ComponentPosition.Left = leftPaddingPercentage;
                } else {
                    childScaledComponent.ComponentPosition.Left = (spacePercentage * barChildren) + leftPaddingPercentage;
                }

                childScaledComponent.Container = GetComponent<RectTransform>();
                childScaledComponent.ComponentScale.Height = heightPercentage;
                childScaledComponent.ComponentScale.Width = widthPercentage;

                // Colour
                childBcc.Colour = UpdateColour(barChildren, transform.childCount - 1);
                ++barChildren;
            }
        }

        public void RandomPopulate(int amount) {
            for (int i = 0; i < amount; i++) {
                AddNewBar(0, 100, Random.Range(10, 101), false);
            }
        }

        public void Purge() {
            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).tag == "Bar") {
                    DestroyImmediate(transform.GetChild(i).gameObject);
                }
            }
        }

        private Color UpdateColour(int i, int children) {
            switch (ChartColourConfig.ChartColourStyle) {
                case BarChartColourConfig.BarChartColourStyle.Solid:
                    return ChartColourConfig.BaseColour;

                case BarChartColourConfig.BarChartColourStyle.SimilarColour:
                    Color simColour = ChartColourConfig.BaseColour;
                    simColour.r = Random.Range(
                        simColour.r - ChartColourConfig.Similarity,
                        simColour.r + ChartColourConfig.Similarity);
                    simColour.g = Random.Range(
                        simColour.g - ChartColourConfig.Similarity,
                        simColour.g + ChartColourConfig.Similarity);
                    simColour.b = Random.Range(
                        simColour.b - ChartColourConfig.Similarity,
                        simColour.b + ChartColourConfig.Similarity);
                    return simColour;

                case BarChartColourConfig.BarChartColourStyle.SimilarShade:
                    Color simShade = ChartColourConfig.BaseColour;
                    if (simShade.r > simShade.g && simShade.r > simShade.b) {
                        simShade.r = Random.Range(
                            simShade.r - ChartColourConfig.Similarity,
                            simShade.r + ChartColourConfig.Similarity);
                    } else if (simShade.g > simShade.r && simShade.g > simShade.b) {
                        simShade.g = Random.Range(
                            simShade.g - ChartColourConfig.Similarity,
                            simShade.g + ChartColourConfig.Similarity);
                    } else if (simShade.b > simShade.r && simShade.b > simShade.g) {
                        simShade.b = Random.Range(
                            simShade.b - ChartColourConfig.Similarity,
                            simShade.b + ChartColourConfig.Similarity);
                    }

                    return simShade;
                case BarChartColourConfig.BarChartColourStyle.Random:
                    return Random.ColorHSV();

                case BarChartColourConfig.BarChartColourStyle.RandomSoft:
                    return Random.ColorHSV(0f, 1f, 0f, 1f, 0.4f, 0.8f);

                case BarChartColourConfig.BarChartColourStyle.RandomRGB:
                    int c = Random.Range(0, 3);
                    switch (c) {
                        case 0:
                            return Color.red;
                        case 1:
                            return Color.blue;
                        case 2:
                            return Color.green;
                        default:
                            Debug.LogException(new ArgumentOutOfRangeException());
                            return Color.black;
                    }

                case BarChartColourConfig.BarChartColourStyle.Gradient:
                    return ChartColourConfig.GardientColour.Evaluate(i / (float)children);

                default:
                    Debug.LogException(new ArgumentOutOfRangeException());
                    return Color.black;
            }
        }

        [Serializable]
        public class BarChartColourConfig {

            public BarChartColourStyle ChartColourStyle;
            public Color BaseColour;
            public Gradient GardientColour;
            [Range(0f, 1f)] public float Similarity;

            public enum BarChartColourStyle {
                /// <summary>
                /// The solid value is the base colour from ChartColourStyle.
                /// </summary>
                Solid,

                /// <summary>
                /// The Similar is colours similar to the base colour.
                /// </summary>
                SimilarShade,

                SimilarColour,

                /// <summary>
                /// The random value is a random colour.
                /// </summary>
                Random,

                /// <summary>
                /// The random soft is random colours within a soft colour looking range.
                /// </summary>
                RandomSoft,

                /// <summary>
                /// The random RGB is either solid Red, Green or Blue .
                /// </summary>
                RandomRGB,

                /// <summary>
                /// The gradient is a gradient between ChartColourStyle start and end colour.
                /// </summary>
                Gradient,
            }
        }
    }

}