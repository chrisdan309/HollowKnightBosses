public abstract class State
{
    protected Enemy boss;

    public State(Enemy boss)
    {
        this.boss = boss;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}