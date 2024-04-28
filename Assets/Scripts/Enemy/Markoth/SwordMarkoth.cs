using UnityEngine;

public class SwordMarkoth : MonoBehaviour
{
    private Transform _playerTransform;
    private Rigidbody2D _rb; 
    public float forceStrength = 50f;
    private bool isAttack = false;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        _playerTransform = player.transform;
        _rb = GetComponent<Rigidbody2D>();

        Invoke("ApplyForceToPlayer", 1f);
    }

    void Update()
    {
        if(!isAttack) LookPlayer();
        if (transform.position.x < minX || transform.position.x > maxX || 
            transform.position.y < minY || transform.position.y > maxY)
        {
            DestroyObject();
        }
    }

    void LookPlayer()
    {
        Vector2 directionToPlayer = _playerTransform.position - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    void ApplyForceToPlayer()
    {
        if(!isAttack){
            Vector2 directionToPlayer = (_playerTransform.position - transform.position).normalized;
            isAttack = true;
            _rb.AddForce(directionToPlayer * forceStrength, ForceMode2D.Impulse);
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(new Vector3(minX, minY, 0), new Vector3(maxX, minY, 0));
        Gizmos.DrawLine(new Vector3(maxX, minY, 0), new Vector3(maxX, maxY, 0));
        Gizmos.DrawLine(new Vector3(maxX, maxY, 0), new Vector3(minX, maxY, 0));
        Gizmos.DrawLine(new Vector3(minX, maxY, 0), new Vector3(minX, minY, 0));
    }
}