using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento
    public float jumpForce = 7.0f; // Fuerza del salto
    private Rigidbody2D _rb; // Referencia al Rigidbody2D
    public bool isGrounded; // Para verificar si el personaje está en el suelo
    public float disableDuration = 1.0f;  // Duración del bloqueo de movimiento
    private bool canMove = true; 
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MovePlayer();
            Jump();            
        }
        
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        if (x != 0)
        {
            float xscale = transform.localScale.x;
            float yscale = transform.localScale.y;
            transform.localScale = new Vector3(Mathf.Sign(xscale) * xscale, yscale, 1);
        }
        
        
        Vector2 movement = new Vector2(x * speed, _rb.velocity.y);
        _rb.velocity = movement;
    }
    
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            StartCoroutine(DisableMovement(disableDuration));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    
    private IEnumerator DisableMovement(float duration) {
        canMove = false;  // Deshabilita el movimiento
        yield return new WaitForSeconds(duration);  // Espera el tiempo especificado
        canMove = true;  // Habilita el movimiento
    }

}
