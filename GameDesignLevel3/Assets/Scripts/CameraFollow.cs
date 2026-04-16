namespace GameDesignLevel3.Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0, 10, 0);
        
        [SerializeField]
        public float smoothSpeed = 0.125f;

        void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}