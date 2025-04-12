using StatePattern.Enemy;

namespace DefaultNamespace
{
    public interface IState
    {
        public OnePunchManController Owner { get; set; }
        void OnStateEnter();
        void OnStateUpdate();
        void OnStateFixedUpdate();
        void OnStateLateUpdate();
        
        void OnStateExit();
    }
}
