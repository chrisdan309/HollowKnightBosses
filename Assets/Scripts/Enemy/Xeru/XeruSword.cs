using System.Collections;
using UnityEngine;

public class XeruSword : MonoBehaviour
{
	public float attackSpeed = 15f;
	public float returnSpeed = 5f;
	public float attackingTime = 1.5f;
	public Vector2 positionOffset;

	public GameObject parentEnemy;
	
	private Rigidbody2D rb;
	private Transform target;

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
	}

	void Update(){
		ReturnSword();
	}

	public void Attack(){
		if (target != null){
			Vector2 direction = (target.position - transform.position).normalized;
			rb.velocity = direction * attackSpeed;
			Debug.Log("Sword attacking towards " + target.name);
			StartCoroutine(ReturnCorrutine());
		}
	}

	public void ReturnSword(){
		if((rb.position - (Vector2)parentEnemy.transform.position != (positionOffset + new Vector2(0.01f, 0.01f))) && target == null){
			
			float step = returnSpeed * Time.deltaTime;
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, positionOffset, step);
		}
	}

	public XeruSword SetPlayerTarget(){
		target = GameObject.FindGameObjectWithTag("Player").transform;
		return this;
	}

	public void RestoreTarget(){
		target = null;
	}

	IEnumerator ReturnCorrutine(){
		yield return new WaitForSeconds(attackingTime);
		RestoreTarget();
		rb.velocity = Vector2.zero;
	}
}