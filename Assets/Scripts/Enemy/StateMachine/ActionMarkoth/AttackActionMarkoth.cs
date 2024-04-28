using UnityEngine;

public class AttackActionMarkoth : Action
{
    private GameObject _player;
    private Rigidbody2D _rb;
    private float _speed = 2.0f;
    
    public AttackActionMarkoth(Rigidbody2D rb, GameObject player)
    {
        _rb = rb;
        _player = player;
    }
    
    public override void Execute(Enemy enemy, float deltaTime)
    {
        
        Vector2 direction = (_player.transform.position - enemy.transform.position).normalized;
        _rb.velocity = direction * _speed;
    }
}
