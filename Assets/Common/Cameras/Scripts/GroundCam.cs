using System;
using UnityEngine;

namespace Assets.Common.Cameras.Scripts {

    [AddComponentMenu("Scripts/Cameras/GroundCam")]
    public class GroundCam : MonoBehaviour {
        public float MBoostSpeed = 10f;
        public float MMoveSpeed = 1f;

        [SerializeField]
        public MouseLook MouseLook;

        private Transform childCameraTransform;
        private Vector2 input;
        private Vector3 moveDir = Vector3.zero;

        private void FixedUpdate() {
            float speed;
            GetInput(out speed);

            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = (transform.forward * input.y) + (transform.right * input.x);

            // get a normal for the surface that is being touched to move along it
            moveDir.x = desiredMove.x * speed;
            moveDir.z = desiredMove.z * speed;

            Vector3 pos = transform.position;
            pos.x += moveDir.x;
            pos.z += moveDir.z;
            transform.position = pos;
            MouseLook.UpdateCursorLock();
            RotateView();
        }

        private void GetInput(out float speed) {
            // Read input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            speed = Input.GetKey(KeyCode.LeftShift) ? MBoostSpeed : MMoveSpeed;
            input = new Vector2(horizontal, vertical);

            if (input.sqrMagnitude > 1) {
                input.Normalize();
            }
        }

        private void RotateView() {
            MouseLook.LookRotation(transform, childCameraTransform);
        }

        // Use this for initialization
        private void Start() {
            childCameraTransform = transform.GetChild(0);
            MouseLook.Init(transform, childCameraTransform);
        }
    }

    [Serializable]
    public class MouseLook {
        public bool ClampVerticalRotation = true;
        public bool LockCursor = true;
        public float MaximumX = 90F;
        public float MinimumX = -90F;
        public bool Smooth;
        public float SmoothTime = 5f;
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        private Quaternion cameraTargetRot;

        private Quaternion characterTargetRot;
        private bool cursorIsLocked = true;

        public void Init(Transform character, Transform camera) {
            characterTargetRot = character.localRotation;
            cameraTargetRot = camera.localRotation;
        }

        public void LookRotation(Transform character, Transform camera, float yRotOptional = 0f) {
            float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

            if (yRotOptional != 0f) {
                characterTargetRot *= Quaternion.Euler(0f, yRotOptional, 0f);
            } else {
                characterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            }

            cameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

            if (ClampVerticalRotation) {
                cameraTargetRot = ClampRotationAroundXAxis(cameraTargetRot);
            }

            if (Smooth) {
                character.localRotation = Quaternion.Slerp(
                    character.localRotation,
                    characterTargetRot,
                    SmoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp(
                    camera.localRotation,
                    cameraTargetRot,
                    SmoothTime * Time.deltaTime);
            } else {
                character.localRotation = characterTargetRot;
                camera.localRotation = cameraTargetRot;
            }

            UpdateCursorLock();
        }

        public void SetCursorLock(bool value) {
            LockCursor = value;
            if (LockCursor) {
                return;
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void UpdateCursorLock() {
            if (LockCursor) {
                InternalLockUpdate();
            }
        }

        private Quaternion ClampRotationAroundXAxis(Quaternion q) {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
            angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);
            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

        private void InternalLockUpdate() {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                cursorIsLocked = false;
            } else if (Input.GetMouseButtonUp(0)) {
                cursorIsLocked = true;
            }

            if (cursorIsLocked) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            } else if (!cursorIsLocked) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

}