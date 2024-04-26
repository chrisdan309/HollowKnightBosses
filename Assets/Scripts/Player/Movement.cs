using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour{

	public float speed = 5.0f;
	public float jumpForce = 7.0f;
	public float doubleJumpForce = 3f;
	public float fallMultiplier = 4f;
	public float lowJumpMultiplier = 3f;
	public float dashSpeed = 25f;
	public float dashDuration = 0.001f;
	public float disableDuration = 1.0f;
	public Color dashColor = Color.red;
	public LayerMask defaultLayer;
	public LayerMask dashLayer;

	private SpriteRenderer spriteRenderer;
	private Rigidbody2D _rb;
	private bool isGrounded;
	private bool canMove = true; 
	private bool canDoubleJump = true;
	private Color originalColor;

	private void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		originalColor = spriteRenderer.color;
	}

	void Start(){
		_rb = GetComponent<Rigidbody2D>();
	}

	void Update(){
		if (canMove){
			MovePlayer();
			Jump();
			if (Input.GetKeyDown(KeyCode.LeftShift)){
				StartCoroutine(Dash());
			}
		}

	}

	void MovePlayer(){
		float x = Input.GetAxis("Horizontal");
		if (x != 0){
			float xscale = Mathf.Sign(x);
			transform.localScale = new Vector3(xscale * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
		}

		//change to force if i have time
		Vector2 movement = new Vector2(x * speed, _rb.velocity.y);
		_rb.velocity = movement;
	}
	
	void Jump(){
		if (Input.GetButtonDown("Jump")){
			if (isGrounded){
				_rb.velocity = new Vector2(_rb.velocity.x, 0);
				_rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
				canDoubleJump = true;
			}
			else if (canDoubleJump){
				_rb.velocity = new Vector2(_rb.velocity.x, 0);
				_rb.AddForce(new Vector2(0, doubleJumpForce), ForceMode2D.Impulse);
				canDoubleJump = false;
			}
		}
		if (_rb.velocity.y < -0.001f){
			_rb.gravityScale = fallMultiplier;
		}
		else if (_rb.velocity.y > 0.001f && !Input.GetButton("Jump")){
			_rb.gravityScale = lowJumpMultiplier;
		}
		else{
			_rb.gravityScale = 1;
		}
	}

	private IEnumerator Dash(){
		Debug.Log(dashLayer);
		spriteRenderer.color = dashColor;
		float direction = transform.right.x * MathF.Sign(transform.localScale.x);
		gameObject.layer = LayerMask.NameToLayer("DashLayer");
		// Change layer to dash layer
		float startTime = Time.time;
		while (Time.time < startTime + dashDuration){
			_rb.velocity = new Vector2(direction * dashSpeed, 0);
			yield return null;
		}
		gameObject.layer = LayerMask.NameToLayer("Player");  // Reset to default layer
		spriteRenderer.color = originalColor;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("Enemy")){
			StartCoroutine(DisableMovement(disableDuration));
		}
	}

	private void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.CompareTag("Ground")){
			isGrounded = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision){
		if (collision.collider.CompareTag("Ground")){
			isGrounded = false;
		}
	}
	
	private IEnumerator DisableMovement(float duration){
		canMove = false;
		yield return new WaitForSeconds(duration);
		canMove = true;
	}
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class Movement : MonoBehaviour
// {
//     public float speed = 5.0f; // Velocidad de movimiento
//     public float jumpForce = 7.0f; // Fuerza del salto
//     private Rigidbody2D _rb; // Referencia al Rigidbody2D
//     public bool isGrounded; // Para verificar si el personaje está en el suelo
//     public float disableDuration = 1.0f;  // Duración del bloqueo de movimiento
//     private bool canMove = true; 
//     // Start is called before the first frame update
//     void Start()
//     {
//         _rb = GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         if (canMove)
//         {
//             MovePlayer();
//             Jump();            
//         }
//         
//     }
//
//     void MovePlayer()
//     {
//         float x = Input.GetAxis("Horizontal");
//         if (x != 0)
//         {
//             float xscale = transform.localScale.x;
//             float yscale = transform.localScale.y;
//             transform.localScale = new Vector3(Mathf.Sign(xscale) * xscale, yscale, 1);
//         }
//         
//         
//         Vector2 movement = new Vector2(x * speed, _rb.velocity.y);
//         _rb.velocity = movement;
//     }
//     
//     void Jump()
//     {
//         if (Input.GetButtonDown("Jump") && isGrounded)
//         {
//             _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
//         }
//     }
//     
//     private void OnTriggerEnter2D(Collider2D other) {
//         if (other.CompareTag("Enemy")) {
//             StartCoroutine(DisableMovement(disableDuration));
//         }
//     }
//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.collider.CompareTag("Ground"))
//         {
//             isGrounded = true;
//         }
//     }
//
//     private void OnCollisionExit2D(Collision2D collision)
//     {
//         if (collision.collider.CompareTag("Ground"))
//         {
//             isGrounded = false;
//         }
//     }
//     
//     private IEnumerator DisableMovement(float duration) {
//         canMove = false;  // Deshabilita el movimiento
//         yield return new WaitForSeconds(duration);  // Espera el tiempo especificado
//         canMove = true;  // Habilita el movimiento
//     }
//
// }
