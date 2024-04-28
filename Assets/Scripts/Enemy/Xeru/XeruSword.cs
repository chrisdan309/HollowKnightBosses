using System.Collections;
using UnityEngine;

public class XeruSword : MonoBehaviour
{
	public float attackSpeed = 15f;
	public float returnSpeed = 5f;
	public float attackingTime = 1.5f;
	public Vector2 positionOffset;

	public GameObject parentEnemy;
	
	private Rigidbody2D _rb;
	private Transform _target;

	void Awake(){
		_rb = GetComponent<Rigidbody2D>();
	}

	void Update(){
		ReturnSword();
	}

	public void Attack(){
		if (_target != null){
			Vector2 direction = (_target.position - transform.position).normalized;
			_rb.velocity = direction * attackSpeed;
			Debug.Log("Sword attacking towards " + _target.name);
			StartCoroutine(ReturnCorrutine());
		}
	}

	public void ReturnSword(){
		if((_rb.position - (Vector2)parentEnemy.transform.position != (positionOffset + new Vector2(0.01f, 0.01f))) && _target == null){
			
			float step = returnSpeed * Time.deltaTime;
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, positionOffset, step);
		}
	}

	public XeruSword SetPlayerTarget(){
		_target = GameObject.FindGameObjectWithTag("Player").transform;
		return this;
	}

	public void RestoreTarget(){
		_target = null;
	}

	IEnumerator ReturnCorrutine(){
		yield return new WaitForSeconds(attackingTime);
		RestoreTarget();
		_rb.velocity = Vector2.zero;
	}
}