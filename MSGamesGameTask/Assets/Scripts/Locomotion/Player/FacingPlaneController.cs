using UnityEngine;

namespace Locomotion.Player
{
    public class FacingPlaneController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;

        void Update()
        {
            transform.position = new Vector3(0f, playerTransform.position.y, 0f);
        }
    }
}
