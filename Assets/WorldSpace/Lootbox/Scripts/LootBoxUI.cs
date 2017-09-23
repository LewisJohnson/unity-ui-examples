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

using UnityEngine;
using UnityEngine.UI;

namespace Assets.WorldSpace.Lootbox.Scripts {

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(LootBoxInventoryHandler))]
    [AddComponentMenu("Scripts/Lootbox/LootBoxUI")]
    public class LootBoxUI : MonoBehaviour {

        public GameObject ListItemGameObject;
        public bool LookAtMainCamera;
        public Transform ScrollViewContentTransform;
        public Transform WorldSpaceCanvasTransform;

        [SerializeField]
        private readonly float smoothFactor = 0f;

        [SerializeField]
        private readonly Vector3 targetPosition = new Vector3(0, 0, 0);

        [SerializeField]
        private bool autoScrollList;

        [SerializeField]
        private bool lockCanvasRotateToYAxis;

        private void OnTriggerEnter(Collider collision) {
            WorldSpaceCanvasTransform.gameObject.SetActive(true);
        }

        private void OnTriggerExit(Collider collision) {
            WorldSpaceCanvasTransform.gameObject.SetActive(false);
        }

        private void Start() {
            GetComponent<LootBoxInventoryHandler>().PopulateList();
            foreach (string item in GetComponent<LootBoxInventoryHandler>().LootBoxInventory) {
                GameObject listItem = Instantiate(ListItemGameObject, ScrollViewContentTransform);
                listItem.transform.Find("Text").GetComponent<Text>().text = item;
            }
        }

        private void Update() {
            if (LookAtMainCamera) {
                WorldSpaceCanvasTransform.LookAt(Camera.main.transform);
                if (lockCanvasRotateToYAxis) {
                    Quaternion t = WorldSpaceCanvasTransform.rotation;
                    t.x = 0;
                    t.z = 0;
                    WorldSpaceCanvasTransform.rotation = t;
                }
            }

            if (autoScrollList) {
                ScrollViewContentTransform.localPosition = Vector3.Lerp(
                    ScrollViewContentTransform.position,
                    targetPosition,
                    Time.deltaTime * smoothFactor);
            }
        }
    }

}