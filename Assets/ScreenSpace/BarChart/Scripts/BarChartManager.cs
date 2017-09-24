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

namespace Assets.ScreenSpace.BarChart.Scripts {

    [AddComponentMenu("Scripts/Bar Chart/Bar Chart Manager")]
    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    public class BarChartManager : MonoBehaviour {
        public BarChartColourStyle BarChartColour = new BarChartColourStyle();

        public GameObject BarChartComponentGameObject;

        [SerializeField] private Slider.Direction barDirection;
        [SerializeField] private int heightPercentage;
        [SerializeField] private int leftPaddingPercentage;
        [SerializeField] private int spacePercentage;
        [SerializeField] private bool wholeNumbers;
        [SerializeField] private int widthPercentage;

        public Slider.Direction BarDirection {
            get { return barDirection; }
            set {
                barDirection = value;
                UpdateVisuals();
            }
        }

        public int HeightPercentage {
            get { return heightPercentage; }
            set {
                heightPercentage = value;
                UpdateVisuals();
            }
        }

        public int LeftPaddingPercentage {
            get { return leftPaddingPercentage; }
            set {
                leftPaddingPercentage = value;
                UpdateVisuals();
            }
        }

        public int SpacePercentage {
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

        public int WidthPercentage {
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
            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).tag != "Bar") {
                    continue;
                }

                if (transform.GetChild(i).GetComponent<Slider>() == null
                    || transform.GetChild(i).GetComponent<ScaledComponent>() == null
                    || transform.GetChild(i).GetComponent<BarChartComponent>() == null) {
                    Debug.LogWarningFormat(
                        transform,
                        "Manager contains children not fit for purpose.",
                        transform.GetChild(i));
                    return;
                }

                Slider childSlider = transform.GetChild(i).GetComponent<Slider>();
                ScaledComponent childScaledComponent = transform.GetChild(i).GetComponent<ScaledComponent>();
                BarChartComponent childBcc = transform.GetChild(i).GetComponent<BarChartComponent>();

                // Slider
                childSlider.direction = barDirection;
                childSlider.wholeNumbers = WholeNumbers;

                // BCC
                childBcc.ValueText.text = childSlider.value.ToString(CultureInfo.CurrentCulture);

                // Scaled comp
                if (i == 0) {
                    childScaledComponent.ComponentPosition.Left = leftPaddingPercentage;
                } else {
                    childScaledComponent.ComponentPosition.Left = (spacePercentage * i) + leftPaddingPercentage;
                }

                childScaledComponent.Container = GetComponent<RectTransform>();
                childScaledComponent.ComponentScale.Height = heightPercentage;
                childScaledComponent.ComponentScale.Width = widthPercentage;
            }
        }

        [Serializable]
        public class BarChartColourStyle {
            [SerializeField] public Color SolidColour;
        }
    }

}