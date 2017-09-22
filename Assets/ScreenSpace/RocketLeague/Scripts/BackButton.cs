﻿/* 
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

namespace Assets.ScreenSpace.RocketLeague.Scripts {
    [AddComponentMenu("Scripts/Rocket League/Back Button")]
    public class BackButton : MonoBehaviour
    {

        public GameObject SubMenuGameObject;
        public GameObject MainMenu;

        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        public void OnClick()
        {
            Animation subAnim = SubMenuGameObject.GetComponent<Animation>();
            subAnim["SubMenuAnimation"].speed = -1;
            subAnim.Play("SubMenuAnimation");

            //Animation menuAnim = MainMenu.GetComponent<Animation>();
            //menuAnim["MenuAnimation"].speed = -1;
            //menuAnim.Play("MenuAnimation");

            //subAnim["SubMenuAnimation"].speed = 1;
            //subAnim["SubMenuAnimation"].time = 0;
            //menuAnim["MenuAnimation"].speed = 1;
            //menuAnim["MenuAnimation"].time = 0;
        }
    }
}
