using System;
using UnityEngine;

namespace Assets.Common.Cameras.Scripts {
    [AddComponentMenu("Scripts/Cameras/GroundCam")]
    public class GroundCam : MonoBehaviour {
        private Transform _mChildCameraTransform;
        private Vector2 _mInput;
        private bool _mIsMoving;
        private Vector3 _mMoveDir = Vector3.zero;
        public float MBoostSpeed = 10f;
        public float MMoveSpeed = 1f;

        [SerializeField]
        public MouseLook MouseLook;

        // Use this for initialization
        private void Start() {
            _mChildCameraTransform = transform.GetChild(0);
            MouseLook.Init(transform, _mChildCameraTransform);
        }

        private void FixedUpdate() {
            float speed;
            GetInput(out speed);

            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * _mInput.y + transform.right * _mInput.x;

            // get a normal for the surface that is being touched to move along it
            _mMoveDir.x = desiredMove.x * speed;
            _mMoveDir.z = desiredMove.z * speed;

            Vector3 pos = transform.position;
            pos.x += _mMoveDir.x;
            pos.z += _mMoveDir.z;
            transform.position = pos;
            MouseLook.UpdateCursorLock();
            RotateView();
        }

        private void RotateView() {
            MouseLook.LookRotation(transform, _mChildCameraTransform);
        }

        private void GetInput(out float speed) {
            // Read input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            speed = Input.GetKey(KeyCode.LeftShift) ? MBoostSpeed : MMoveSpeed;
            _mInput = new Vector2(horizontal, vertical);

            if (_mInput.sqrMagnitude > 1) _mInput.Normalize();
        }
    }

    [Serializable]
    public class MouseLook {
        private bool _mCursorIsLocked = true;
        public bool ClampVerticalRotation = true;
        public bool LockCursor = true;
        private Quaternion m_CameraTargetRot;

        private Quaternion m_CharacterTargetRot;
        public float MaximumX = 90F;
        public float MinimumX = -90F;
        public bool Smooth;
        public float SmoothTime = 5f;
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;

        public void Init(Transform character, Transform camera) {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
        }


        public void LookRotation(Transform character, Transform camera, float yRotOptional = 0f) {
            float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

            if (yRotOptional != 0f) m_CharacterTargetRot *= Quaternion.Euler(0f, yRotOptional, 0f);
            else m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);

            m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

            if (ClampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

            if (Smooth) {
                character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
                    SmoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot,
                    SmoothTime * Time.deltaTime);
            } else {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }

            UpdateCursorLock();
        }

        public void SetCursorLock(bool value) {
            LockCursor = value;
            if (!LockCursor) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void UpdateCursorLock() {
            if (LockCursor) InternalLockUpdate();
        }

        private void InternalLockUpdate() {
            if (Input.GetKeyUp(KeyCode.Escape)) _mCursorIsLocked = false;
            else if (Input.GetMouseButtonUp(0)) _mCursorIsLocked = true;

            if (_mCursorIsLocked) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            } else if (!_mCursorIsLocked) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
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
    }
}