using UnityEngine;
using UnityEngine.UI;

namespace Assets.WorldSpace.Lootbox.Scripts {
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(LootBoxInventoryHandler))]

    public class LootBoxUI : MonoBehaviour {

        [SerializeField]
        private Vector3 targetPosition = new Vector3(0, 0, 0);
        [SerializeField]
        private float smoothFactor = 0f;
        [SerializeField]
        private bool _lockCanvasRotateToYAxis;
        [SerializeField]
        private bool _autoScrollList;

        public GameObject ListItemGameObject;
        public Transform ScrollViewContentTransform;
        public Transform WorldSpaceCanvasTransform;
        public bool LookAtMainCamera;

        private void Start() {
            GetComponent<LootBoxInventoryHandler>().PopulateList();
            foreach (string item in GetComponent<LootBoxInventoryHandler>().LootBoxInventory) {
                var listItem = Instantiate(ListItemGameObject, ScrollViewContentTransform);
                listItem.transform.Find("Text").GetComponent<Text>().text = item;
            }
        }

        private void Update() {
            if (LookAtMainCamera) {
                WorldSpaceCanvasTransform.LookAt(Camera.main.transform);
                if (_lockCanvasRotateToYAxis) {
                    var t = WorldSpaceCanvasTransform.rotation;
                    t.x = 0;
                    t.z = 0;
                    WorldSpaceCanvasTransform.rotation = t;
                }
            }

            if (_autoScrollList) {
                ScrollViewContentTransform.localPosition = Vector3.Lerp(ScrollViewContentTransform.position, targetPosition, Time.deltaTime * smoothFactor);
            }
        }

        private void OnTriggerEnter(Collider collision) {
            WorldSpaceCanvasTransform.gameObject.SetActive(true);
        }

        private void OnTriggerExit(Collider collision) {
            WorldSpaceCanvasTransform.gameObject.SetActive(false);
        }
    }
}
