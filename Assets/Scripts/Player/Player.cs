using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	public TMP_Text healthText;
	public int life = 3;
	public float invulnerabilityTime = 2f;
	[SerializeField] 
	private bool isInvulnerable = false;
	private Rigidbody2D _rb;
	private int _defaultLayer;
	
	private SpriteRenderer _spriteRenderer;
	public Color invulnerableColor = Color.blue;
	private Color _originalColor;

	private void Awake(){
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_originalColor = _spriteRenderer.color;
		_rb = GetComponent<Rigidbody2D>();
		_defaultLayer = gameObject.layer;
		UpdateHealthText();
	}

	public void OnTriggerEnter2D(Collider2D collision){

		if (!isInvulnerable){
			life--;
			UpdateHealthText();
			MakeInvulnerable();
			StartCoroutine(PushPlayer(collision));
			// PushPlayer(collision);
			if (life <= 0){
				Destroy(gameObject);
			}
		}
	}

	private void MakeInvulnerable(){
		isInvulnerable = true;
		_spriteRenderer.color = invulnerableColor;

		Invoke("RemoveInvulnerability", invulnerabilityTime);
	}

	private void RemoveInvulnerability(){
		isInvulnerable = false;
		_spriteRenderer.color = _originalColor;
		gameObject.layer = _defaultLayer;
	}

	void UpdateHealthText()
	{
		healthText.text = "Vida: " + life.ToString("0");
	}
	
	IEnumerator PushPlayer(Collider2D collision){
		float direction = (transform.position.x - collision.transform.position.x) > 0 ? -1 : 1;
		//Debug.Log(direction > 0 ? "Izquierda" : "Derecha");
		
		_rb.AddForce(new Vector2(direction * 0.001f, 0), ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.8f);
		_rb.velocity = new Vector2(0,0);

	}
	
	

}
