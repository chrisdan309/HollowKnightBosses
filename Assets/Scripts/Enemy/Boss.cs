using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    protected State currentState;

    public abstract void ChangeState(State newState);

    protected virtual void Update()
    {
        currentState?.Execute();
    }
}