using UnityEngine;

namespace Assets.ScreenSpace.RocketLeague.Scripts
{
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
