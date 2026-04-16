using UnityEngine;
using UnityEngine.InputSystem;
namespace GameDesignLevel3.Assets.Scripts
{
    public class Box : MonoBehaviour
    {
        [SerializeField] private float interactDistance = 3f;
        private Transform _playerTransform;

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _playerTransform = player.transform;
            }
            Debug.Log("Found player");
        }

        private void Update()
        {
            if (_playerTransform == null) return;
            
            float distance = Vector3.Distance(_playerTransform.position, transform.position);
            //Debug.Log(distance);
            if (distance <= interactDistance)
            {
                if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        
    }
}