using UnityEngine;

namespace Assets.Common.StandardAssets.Utility
{
    [AddComponentMenu("Scripts/Standard Assets/Utility/Timed Object Destructor")]
    public class TimedObjectDestructor : MonoBehaviour
    {
        [SerializeField] private float m_TimeOut = 1.0f;
        [SerializeField] private bool m_DetachChildren = false;


        private void Awake()
        {
            Invoke("DestroyNow", m_TimeOut);
        }


        private void DestroyNow()
        {
            if (m_DetachChildren)
            {
                transform.DetachChildren();
            }

            DestroyObject(gameObject);
        }
    }
}
