using UnityEngine;

namespace  GameDesignLevel3.Assets.Scripts
{
    public abstract class DesiredVelocityProvider : MonoBehaviour
    {
        [SerializeField, Range(0, 100)] 
        private float weight = 1f;

        private bool isActive = true;
        public float Weight => isActive ? weight : 0f;
        
        protected Vehicle Vehicle;

        private void Awake()
        {
            Vehicle = GetComponentInParent<Vehicle>();
        }
        
        public abstract Vector3 GetDesiredVelocity();

        public void Activate()
        {
            isActive = true;
        }

        public void Deactivate()
        {
            isActive = false;
        }

        public virtual  bool IsSatisfied()
        {
            return GetDesiredVelocity().magnitude < 30f;
        }
    }
}