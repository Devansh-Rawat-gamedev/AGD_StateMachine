using StatePattern.Enemy;
using StatePattern.Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class IdleState : IState
    {
        private readonly OnePunchManStateMachine stateMachine;
        public OnePunchManController Owner { get; set; }
        private float timer;

        public IdleState(OnePunchManStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        

        public void OnStateEnter() => ResetTimer();
        public void OnStateUpdate()
        {
        }

        public void OnStateFixedUpdate()
        {
        }

        public void OnStateLateUpdate()
        {
        }

        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                stateMachine.ChangeState(OnePunchManStates.ROTATING);
        }

        public void OnStateExit() => timer = 0;

        private void ResetTimer() => timer = Owner.Data.IdleTime;
    }
}
