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

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScreenSpace.BarChart.Scripts {

    [AddComponentMenu("Scripts/Bar Chart/Bar Chart Editor")]
    [CustomEditor(typeof(BarChartManager))]
    public class BarChartEditor : Editor {
        private int max = 100;
        private int min = 0;
        private bool showValue = true;
        private bool edit = true;
        private int value = 50;
        private int randomAmount = 99;

        public override void OnInspectorGUI() {
            BarChartManager bcmScript = (BarChartManager)target;
            EditorGUILayout.LabelField(
                "Bar Chart Manager",
                new GUIStyle { fontSize = 18, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            DrawDefaultInspector();

            if (GUILayout.Button("Update Styles")) {
                bcmScript.UpdateVisuals();
            }

            randomAmount = EditorGUILayout.IntField(randomAmount);
            if (GUILayout.Button("Random")) {
                bcmScript.RandomPopulate(randomAmount);
            }

            EditorGUILayout.Space();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField(
                "Bar Chart Editor",
                new GUIStyle { fontSize = 18, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
            EditorGUILayout.LabelField("New", new GUIStyle { fontSize = 12, fontStyle = FontStyle.Bold });

            value = EditorGUILayout.IntField("Value", value);
            min = EditorGUILayout.IntField("Min", min);
            max = EditorGUILayout.IntField("Max", max);
            showValue = EditorGUILayout.Toggle("Show value text", showValue);

            if (GUILayout.Button("Create")) {
                bcmScript.AddNewBar(min, max, value, showValue);
            }

            EditorGUILayout.LabelField("Edit", new GUIStyle { fontSize = 12, fontStyle = FontStyle.Bold });
            if (GUILayout.Button("Purge")) {
                bcmScript.Purge();
            }
            edit = EditorGUILayout.Foldout(edit, "Show Children");
            if (edit) {
                for (int i = 0; i < bcmScript.transform.childCount; i++) {
                    Transform child = bcmScript.transform.GetChild(i);
                    if (child.tag != "Bar") {
                        continue;
                    }

                    EditorGUILayout.LabelField(
                        string.Format("Bar {0}", i),
                        new GUIStyle { fontSize = 10, fontStyle = FontStyle.Bold });

                    Slider childSlider = child.GetComponent<Slider>();
                    BarChartComponent childScript = child.GetComponent<BarChartComponent>();

                    childSlider.value = EditorGUILayout.Slider(
                        "Value",
                        childSlider.value,
                        childSlider.minValue,
                        childSlider.maxValue);
                    childScript.ShowValueText = EditorGUILayout.Toggle("Show value text", childScript.ShowValueText);
                    childScript.Colour = EditorGUILayout.ColorField("Colour", childScript.Colour);

                    if (GUILayout.Button("Delete")) {
                        DestroyImmediate(child.gameObject);
                    }

                    EditorGUILayout.Separator();
                    EditorGUILayout.Space();
                }
            }
        }
    }

}

#endif