namespace GameDesignLevel3.Assets.Scripts
{
    using System.Linq;
    using UnityEngine;
    
    public class Vehicle : MonoBehaviour
    {
        private Vector3 velocity;
        private Vector3 acceleration;

        [SerializeField] 
        private float mass = 1;
        
        [SerializeField]
        private float frictionValue = 0.5f;
        
        [SerializeField,  Range(0, 100)]
        private float velocityLimit = 10;
        
        [SerializeField,  Range(0, 200)]
        private float steeringForceLimit = 20;

        [SerializeField] 
        private float epsilon = 0.05f;

        public float VelocityLimit => velocityLimit;
        public float SteeringForceLimit => steeringForceLimit;
        
        public Vector3 Velocity => velocity;

        public void ApplyForce(Vector3 force)
        {
            force /= mass;
            acceleration += force;
        }

        public void Update()
        {
            ApplyFriction();
            ApplySteeringForce();
            ApplyForces();
            void ApplyFriction()
            {
                if (velocity.magnitude < epsilon)
                {
                    return;
                }
                
                var friction = -velocity.normalized * frictionValue;
                ApplyForce(friction );
            }

            void ApplySteeringForce()
            {
                var providers = GetComponents<DesiredVelocityProvider>();
                var weightedDesired = Vector3.zero;
                var totalWeight = 0f;

                foreach (var provider in providers.Where(provider => provider.enabled))
                {
                    var desired = provider.GetDesiredVelocity();
                    var w = provider.Weight;
                    weightedDesired += desired * w;
                    totalWeight += w;
                }

                if (totalWeight < 0.001f)
                {
                    return;
                }
                
                var desiredVelocity = weightedDesired / totalWeight;
                var steering = desiredVelocity - velocity;
                ApplyForce(Vector3.ClampMagnitude(steering, steeringForceLimit));
            }

            void ApplyForces()
            {
                velocity += acceleration * Time.deltaTime;
                velocity = Vector3.ClampMagnitude(velocity, velocityLimit);

                if (velocity.magnitude < epsilon)
                {
                    velocity = Vector3.zero;
                }
                else
                {
                    Vector3 lookDirection = new Vector3(velocity.x, 0, velocity.z);
                    if (lookDirection != Vector3.zero)
                    {
                        transform.rotation = Quaternion.LookRotation(lookDirection);
                    }
                    //bool movingForward = Vector3.Dot(velocity, transform.forward) >= 0f;
                    //transform.rotation = Quaternion.LookRotation(velocity);
                    transform.position += velocity * Time.deltaTime;
                }
                acceleration = Vector3.zero;
            }
        }
    }
}