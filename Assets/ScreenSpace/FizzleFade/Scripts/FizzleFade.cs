using System;
using UnityEngine;

namespace Assets.ScreenSpace.FizzleFade.Scripts {
    [ExecuteInEditMode]
    public class FizzleFade : MonoBehaviour
    {

        public bool lol;
        public void Start() {
            
        }

        public void StartFizzleFade(Color pixelColour)
        {
            var cont = transform.GetComponent<Assets.ScreenSpace.FizzleFade.Scripts.Container>();
            for (int i = 0; i < (int)Math.Ceiling(cont.Width); i++)
            {
                Debug.Log(i);
            }
        }

        public void Update()
        {
            if (lol)
            {
                StartFizzleFade(new Color(255, 0, 0, 255));
            }   
        }
    }
}


