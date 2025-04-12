using UnityEngine;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {
        private bool isIdle;
        private bool isRotating;
        private bool isShooting;
        private float idleTimer;
        private float shootTimer;
        private float targetRotation;
        private PlayerController target;


        private OnePunchManStateMachine stateMachine;
     
        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(OnePunchManStates.IDLE);
        }
                                         
        private void CreateStateMachine() => stateMachine = new OnePunchManStateMachine(this);

        private void InitializeVariables()
        {
            isIdle = true;
            isRotating = false;
            isShooting = false;
            idleTimer = enemyScriptableObject.IdleTime;
            shootTimer = enemyScriptableObject.RateOfFire;
        }

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            if(isIdle && !isRotating && !isShooting)
            {
                idleTimer -= Time.deltaTime;
                if(idleTimer <= 0)
                {
                    isIdle = false;
                    isRotating = true;
                    targetRotation = (Rotation.eulerAngles.y + 180) % 360;
                }
            }

            if(!isIdle && isRotating && !isShooting)
            {
                SetRotation(CalculateRotation());
                if(IsRotationComplete())
                {
                    isIdle = true;
                    isRotating = false;
                    ResetTimer();
                }
            }

            if(!isIdle && !isRotating && isShooting)
            {
                

            }

        }

        public void ResetTimer() => idleTimer = enemyScriptableObject.IdleTime;

        public Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(Rotation.eulerAngles.y, targetRotation, enemyScriptableObject.RotationSpeed * Time.deltaTime);

        public bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Data.RotationThreshold;

        public bool IsFacingPlayer(Quaternion desiredRotation) => Quaternion.Angle(Rotation, desiredRotation) < Data.RotationThreshold;

        public Quaternion CalculateRotationTowardsPlayer()
        {
            Vector3 directionToPlayer = target.Position - Position;
            directionToPlayer.y = 0f;
            return Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }

        public Quaternion RotateTowards(Quaternion desiredRotation) => Quaternion.LerpUnclamped(Rotation, desiredRotation, enemyScriptableObject.RotationSpeed / 30 * Time.deltaTime);

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            isIdle = false;
            isRotating = false;
            isShooting = true;
            target = targetToSet;
            shootTimer = 0;
        }

        public override void PlayerExitedRange() 
        {
            isIdle = true;
            isRotating = false;
            isShooting = false;
        }
    }
    
    public enum OnePunchManStates
    {
        IDLE=0,
        ROTATING=1,
        SHOOTING=2
    }
}
