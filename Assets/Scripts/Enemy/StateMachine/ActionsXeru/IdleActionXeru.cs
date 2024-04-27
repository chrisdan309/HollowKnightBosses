using UnityEngine;

public class IdleActionXeru : Action{

	private float timeCounter = 0; 
	public float speed = 2.0f; 
	public float width = 13f; 
	public float height = 1f;
	private bool direction = true;

	private Rigidbody2D rb;

	public IdleActionXeru(Rigidbody2D rb){
		direction = true;
		this.rb = rb;
	}

	public override void Execute(Enemy enemy, float DeltaTime){
		// timeCounter += Time.deltaTime * speed;

		// float x = Mathf.Cos(timeCounter/5) * width;
		//float y = 5 + Mathf.Sin(timeCounter) * height;

		// enemy.transform.position = new Vector2(x, enemy.transform.position.y);
		Debug.Log("Xeru is idle at position: " + rb.position);
	}
}