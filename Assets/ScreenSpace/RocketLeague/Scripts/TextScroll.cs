using System.Collections.Generic;
using UnityEngine;

// Modified from: https://gist.github.com/Jiyambi/02ea110950b503e3f4aa

namespace Assets.ScreenSpace.RocketLeague.Scripts
{
    [ExecuteInEditMode]
    public class TextScroll : MonoBehaviour
    {
        [SerializeField] private int _mNumRepeats = 0;
        [SerializeField] private bool _mRepeatWhenEmpty = true;
        [SerializeField] private bool _mAutoRemove = false;
        [SerializeField] private TextMesh _mTextMesh;
        [SerializeField] private float _mSpeed = 1;
        [SerializeField] private float _mScissorMin = -4;
        [SerializeField] private float _mScissorMax = 4;
        [SerializeField] private readonly List<string> _mMessages = new List<string>
        {
            "WELCOME TO UNITY USER INTERFACE EXAMPLES!",
            "Github: https://github.com/LewisJohnson/unity-ui-examples",
            "Follow me on Twitter @lewisj489"
        };

        private int _mCurrentNumRepeats;

        void Start()
        {
            _mTextMesh.text = _mMessages.Count > 0 ? _mMessages[0] : "";
        }


        void Update(){
            _mTextMesh.transform.position -= new Vector3(_mSpeed*Time.deltaTime, 0, 0);

            if (_mTextMesh.GetComponent<Renderer>().bounds.max.x < _mScissorMin){
                UseNextMessage();
            }
        }


        private void UseNextMessage(){
            bool loadNew = _mCurrentNumRepeats >
                           _mNumRepeats && _mNumRepeats != 0 ||
                           !(_mRepeatWhenEmpty && _mMessages.Count <= 1) ||
                           _mTextMesh.text == "" ||
                           _mTextMesh.text != _mMessages[0];

            if (!loadNew){
                ++_mCurrentNumRepeats;
            }

            if (loadNew){
                if (!_mAutoRemove){
                    _mMessages.Add(_mMessages[0]);
                }

                _mMessages.RemoveAt(0);
                _mCurrentNumRepeats = 0;
                _mTextMesh.text = _mMessages.Count > 0 ? _mMessages[0] : "";
            }

            float width = _mTextMesh.GetComponent<Renderer>().bounds.max.x
                          - _mTextMesh.GetComponent<Renderer>().bounds.min.x;

            _mTextMesh.transform.position =
                new Vector3(_mScissorMax + width/2,
                    _mTextMesh.transform.position.y,
                    _mTextMesh.transform.position.z);
        }

        public void AddMessage(string newMessage)
        {
            _mMessages.Add(newMessage);
            if (_mTextMesh.text == "")
                UseNextMessage();
        }

        public void RemoveMessage(string newMessage)
        {
            _mMessages.Remove(newMessage);
        }
    }
}