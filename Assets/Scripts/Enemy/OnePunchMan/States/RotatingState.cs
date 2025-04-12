using StatePattern.Enemy;
using UnityEngine;

namespace DefaultNamespace
{
    public class RotatingState : IState
    {
        private readonly OnePunchManStateMachine stateMachine;
        private float targetRotation;
        public OnePunchManController Owner { get; set; }
        
        public RotatingState(OnePunchManStateMachine stateMachine)
        {
            this.stateMachine= stateMachine;
        }
        public void OnStateEnter() => targetRotation = (Owner.Rotation.eulerAngles.y + 180) % 360;

        public void OnStateUpdate()
        {
            Owner.SetRotation(CalculateRotation());
            if (IsRotationComplete())
                stateMachine.ChangeState(OnePunchManStates.IDLE);
        }

        public void OnStateFixedUpdate()
        {
            // Code to execute during fixed update in the rotating state
        }

        public void OnStateLateUpdate()
        {
            // Code to execute during late update in the rotating state
        }

        public void OnStateExit() => targetRotation = 0;

        private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(Owner.Rotation.eulerAngles.y, targetRotation, Owner.Data.RotationSpeed * Time.deltaTime);

        private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Owner.Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Owner.Data.RotationThreshold;
    }
}
