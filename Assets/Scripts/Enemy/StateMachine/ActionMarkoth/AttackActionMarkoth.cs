using UnityEngine;

public class AttackActionMarkoth : Action
{
    private GameObject _player;
    private Rigidbody2D _rb;
    
    public AttackActionMarkoth(Rigidbody2D rb, GameObject player)
    {
        this._rb = rb;
        this._player = player;
    }
    
    public override void Execute(Enemy enemy, float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}
