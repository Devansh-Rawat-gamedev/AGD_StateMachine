using StatePattern.Enemy;
using UnityEngine;

namespace DefaultNamespace
{
    public class ShootingState : IState
    {
        private readonly OnePunchManStateMachine stateMachine;
        private float timer;
        public OnePunchManController Owner { get; set; }
        
        public ShootingState(OnePunchManStateMachine stateMachine)
        {
            this.stateMachine= stateMachine;
        }
        public void OnStateEnter()
        {
            ResetTimer();
        }

        public void OnStateUpdate()
        {
            Quaternion desiredRotation = Owner.CalculateRotationTowardsPlayer();
            Owner.SetRotation(Owner.RotateTowards(desiredRotation));
                
            if(Owner.IsFacingPlayer(desiredRotation))
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = Owner.enemyScriptableObject.RateOfFire;
                    Owner.Shoot();
                }
            }
        }

        public void OnStateFixedUpdate()
        {
            
        }

        public void OnStateLateUpdate()
        {
            
        }

        public void OnStateExit()
        {
            
        }

        void ResetTimer() => timer = Owner.Data.IdleTime;
    }
}
