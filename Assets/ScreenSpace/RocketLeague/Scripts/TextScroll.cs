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
using System.Collections.Generic;
using UnityEngine;

// Modified from: https://gist.github.com/Jiyambi/02ea110950b503e3f4aa
namespace Assets.ScreenSpace.RocketLeague.Scripts
{
    [ExecuteInEditMode]
    [AddComponentMenu("Scripts/Rocket League/Text Scroll")]
    public class TextScroll : MonoBehaviour
    {
        [SerializeField] private int mNumRepeats = 0;
        [SerializeField] private bool mRepeatWhenEmpty = true;
        [SerializeField] private bool mAutoRemove = false;
        [SerializeField] private TextMesh mTextMesh;
        [SerializeField] private float mSpeed = 1;
        [SerializeField] private float mScissorMin = -4;
        [SerializeField] private float mScissorMax = 4;
        [SerializeField] private readonly List<string> mMessages = new List<string>
        {
            "WELCOME TO UNITY USER INTERFACE EXAMPLES!",
            "Github: https://github.com/LewisJohnson/unity-ui-examples",
            "Follow me on Twitter @lewisj489"
        };

        private int mCurrentNumRepeats;

        void Start()
        {
            mTextMesh.text = mMessages.Count > 0 ? mMessages[0] : string.Empty;
        }


        void Update(){
            mTextMesh.transform.position -= new Vector3(mSpeed*Time.deltaTime, 0, 0);

            if (mTextMesh.GetComponent<Renderer>().bounds.max.x < mScissorMin){
                UseNextMessage();
            }
        }


        private void UseNextMessage(){
            bool loadNew = mCurrentNumRepeats >
                           mNumRepeats && mNumRepeats != 0 ||
                           !(mRepeatWhenEmpty && mMessages.Count <= 1) ||
                           mTextMesh.text == string.Empty ||
                           mTextMesh.text != mMessages[0];

            if (!loadNew){
                ++mCurrentNumRepeats;
            }

            if (loadNew){
                if (!mAutoRemove){
                    mMessages.Add(mMessages[0]);
                }

                mMessages.RemoveAt(0);
                mCurrentNumRepeats = 0;
                mTextMesh.text = mMessages.Count > 0 ? mMessages[0] : string.Empty;
            }

            float width = mTextMesh.GetComponent<Renderer>().bounds.max.x
                          - mTextMesh.GetComponent<Renderer>().bounds.min.x;

            mTextMesh.transform.position =
                new Vector3(mScissorMax + width/2,
                    mTextMesh.transform.position.y,
                    mTextMesh.transform.position.z);
        }

        public void AddMessage(string newMessage)
        {
            mMessages.Add(newMessage);
            if (mTextMesh.text == string.Empty)
                UseNextMessage();
        }

        public void RemoveMessage(string newMessage)
        {
            mMessages.Remove(newMessage);
        }
    }
}