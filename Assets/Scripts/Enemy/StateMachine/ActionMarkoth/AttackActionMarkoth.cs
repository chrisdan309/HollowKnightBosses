using UnityEngine;

public class AttackActionMarkoth : Action
{
    private GameObject _player;
    private float _acelerationToPlayer;
    private Rigidbody2D _rb;
    private float _maxSpeed = 2f;

    public AttackActionMarkoth(Rigidbody2D rb, GameObject player)
    {
        _acelerationToPlayer = 3f;
        _rb = rb;
        _player = player;
    }

    public override void Execute(Enemy enemy, float deltaTime)
    {
        if (_player == null || enemy == null) return;
        
        Vector2 direction = (_player.transform.position - enemy.transform.position).normalized;
        
        if (_rb.velocity == Vector2.zero || _rb.velocity.magnitude < 0.1) 
        {
            _rb.velocity = direction * _maxSpeed;
        }
        else
        {
            _rb.AddForce(direction * _acelerationToPlayer, ForceMode2D.Force);
        
            if (_rb.velocity.magnitude > _maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * _maxSpeed;
            }
        }
        
    }
}