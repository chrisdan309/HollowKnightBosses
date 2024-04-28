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

	private SpriteRenderer _spriteRenderer;
	private Rigidbody2D _rb;
	private bool _isGrounded;
	private bool _canMove = true; 
	private bool _canDoubleJump = true;
	private Color _originalColor;

	private void Awake(){
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_originalColor = _spriteRenderer.color;
	}

	void Start(){
		_rb = GetComponent<Rigidbody2D>();
	}

	void Update(){
		if (_canMove){
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
			if (_isGrounded){
				_rb.velocity = new Vector2(_rb.velocity.x, 0);
				_rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
				_canDoubleJump = true;
			}
			else if (_canDoubleJump){
				_rb.velocity = new Vector2(_rb.velocity.x, 0);
				_rb.AddForce(new Vector2(0, doubleJumpForce), ForceMode2D.Impulse);
				_canDoubleJump = false;
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
		_spriteRenderer.color = dashColor;
		float direction = transform.right.x * MathF.Sign(transform.localScale.x);
		gameObject.layer = LayerMask.NameToLayer("DashLayer");
		// Change layer to dash layer
		float startTime = Time.time;
		while (Time.time < startTime + dashDuration){
			_rb.velocity = new Vector2(direction * dashSpeed, 0);
			yield return null;
		}
		gameObject.layer = LayerMask.NameToLayer("Player");  // Reset to default layer
		_spriteRenderer.color = _originalColor;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("Enemy")){
			StartCoroutine(DisableMovement(disableDuration));
		}
	}

	private void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.CompareTag("Ground")){
			_isGrounded = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision){
		if (collision.collider.CompareTag("Ground")){
			_isGrounded = false;
		}
	}
	
	private IEnumerator DisableMovement(float duration){
		_canMove = false;
		yield return new WaitForSeconds(duration);
		_canMove = true;
	}
}

