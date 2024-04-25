using System.Collections.Generic;

public class State
{
    public List<Action> actions = new List<Action>();
    public EnemyState stateType;

    public State(EnemyState type)
    {
        stateType = type;
    }

    public void ExecuteStateActions(Enemy enemy)
    {
        foreach (var action in actions)
        {
            action.Execute(enemy);
        }
    }

    public void AddAction(Action action)
    {
        actions.Add(action);
    }
}