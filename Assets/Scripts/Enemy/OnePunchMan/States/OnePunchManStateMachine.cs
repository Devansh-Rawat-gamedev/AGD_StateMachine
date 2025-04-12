using System.Collections.Generic;
using DefaultNamespace;
using StatePattern.Enemy;

public class OnePunchManStateMachine
{
    private OnePunchManController owner;
    
    protected Dictionary<OnePunchManStates, IState> States = new();
    private IState currentState;

    public OnePunchManStateMachine(OnePunchManController owner)
    {
        this.owner = owner;
        CreateStates();
        SetOwner();
    }

    private void CreateStates()
    {
        States.Add(OnePunchManStates.IDLE, new IdleState(this));
        States.Add(OnePunchManStates.ROTATING, new RotatingState(this));
        States.Add(OnePunchManStates.SHOOTING, new ShootingState(this));
    }
    
    private void SetOwner()
    {
        foreach (IState state in States.Values)
        {
            state.Owner = owner;
        }
    }

    public void Update() => currentState?.OnStateUpdate();
    public void FixedUpdate() => currentState?.OnStateFixedUpdate();
    public void LateUpdate() => currentState?.OnStateLateUpdate();
    

    protected void ChangeState(IState newState)
    {
        currentState?.OnStateExit();
        currentState = newState;
        currentState?.OnStateEnter();
    }
    public void ChangeState(OnePunchManStates newState) => ChangeState(States[newState]);
}
