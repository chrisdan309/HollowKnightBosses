using System.Collections;
using UnityEngine;

public class BounceAction : Action
{
    private float bounceForce;
    private Rigidbody2D rb;

    public BounceAction(float bounceForce, Rigidbody2D rb)
    {
        this.bounceForce = bounceForce;
        this.rb = rb;
    }

    public override void Execute(Enemy enemy)
    {
        Vector2 forceDirection = new Vector2(-1f, 0).normalized;
        rb.AddForce(forceDirection * bounceForce, ForceMode2D.Impulse);
    }
}
