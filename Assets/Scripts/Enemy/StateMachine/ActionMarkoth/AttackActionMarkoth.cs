using UnityEngine;

public class AttackActionMarkoth : Action
{
    private GameObject _player;
    private float _acelerationToPlayer;
    private Rigidbody2D _rb;
    private float _maxSpeed = 1f;
    private GameObject _prefab;
    private float _spawnInterval = 2.5f; 
    private float _timeSinceLastSpawn;

    public AttackActionMarkoth(Rigidbody2D rb, GameObject player, GameObject prefab)
    {
        _acelerationToPlayer = 3f;
        _rb = rb;
        _player = player;
        _prefab = prefab;
    }

    public override void Execute(Enemy enemy)
    {
        if (_player == null || enemy == null) return;
        FollowPlayer(enemy);
        SpawnPrefab();
    }

    private void FollowPlayer(Enemy enemy)
    {
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

    private void SpawnPrefab()
    {
        _timeSinceLastSpawn += Time.deltaTime;
        if (_timeSinceLastSpawn >= _spawnInterval)
        {
            _timeSinceLastSpawn = 0;


            Vector3 spawnPosition = _player.transform.position +
                                    (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), -1f)) * 10f;
            
            Object.Instantiate(_prefab, spawnPosition, Quaternion.identity);
        }
    }
}
