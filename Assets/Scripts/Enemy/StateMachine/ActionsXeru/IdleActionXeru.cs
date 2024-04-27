using UnityEngine;

public class IdleActionXeru : Action
{
    private Rigidbody2D rb;

    private float timeCounter = 0; 
    public float speed = 2.0f; 
    public float width = 13f; 
    public float height = 1f;

    public IdleActionXeru(Rigidbody2D rb)
    {
        this.rb = rb;
    }

    public override void Execute(Enemy enemy)
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;
        float y = 5 + Mathf.Sin(timeCounter) * height;

        rb.position = new Vector2(x, y);
        Debug.Log("Xeru is idle at position: " + rb.position);
    }
}