using UnityEngine;

public class IdleActionXeru : Action{

	private float timeCounter = 0; 
	public float speed = 2.0f; 
	public float width = 13f; 
	public float height = 1f;
	private int direction = 1;

	private Rigidbody2D rb;

	public IdleActionXeru(Rigidbody2D rb){
		direction = 1;
		this.rb = rb;
	}

	public override void Execute(Enemy enemy){
		// timeCounter += Time.deltaTime * speed;

		rb.velocity = new Vector2(speed, 0) * direction;
		if(enemy.transform.position.x >= 13){
			direction = -1;
		}
		else if (enemy.transform.position.x <= -13){
			direction = 1;
		}

		// float x = Mathf.Cos(timeCounter/5) * width;
		//float y = 5 + Mathf.Sin(timeCounter) * height;

		// enemy.transform.position = new Vector2(x, enemy.transform.position.y);
		// Debug.Log("Xeru is idle at position: " + enemy.transform.position);
	}
}