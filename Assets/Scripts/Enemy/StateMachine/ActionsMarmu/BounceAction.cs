using System.Collections;
using UnityEngine;

public class BounceAction : Action {
	private float acelerationToPlayer;
	private GameObject player;
	private Rigidbody2D rb;

	public BounceAction(float acelerationToPlayer, Rigidbody2D rb, GameObject player) {
		this.acelerationToPlayer = acelerationToPlayer;
		this.rb = rb;
		this.player = player;
	}

	public override void Execute(Enemy enemy, float deltaTime) {
		// Vector2 forceDirection = new Vector2(-1f, 0).normalized;
		// apply aceleration to player
		if (player == null || enemy == null) return;

		Vector2 direction = player.transform.position - enemy.transform.position;
		rb.AddForce(direction.normalized * acelerationToPlayer, ForceMode2D.Force);
		Debug.Log(rb.totalForce);
		// Vector2 forceDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
		// rb.AddForce(forceDirection * bounceForce, ForceMode2D.Impulse);
		
	}
	
}
