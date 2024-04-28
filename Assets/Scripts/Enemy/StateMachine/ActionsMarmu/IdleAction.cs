using System.Collections;
using UnityEngine;

public class IdleAction : Action
{
    private Rigidbody2D rb;

    public IdleAction(Rigidbody2D rb)
    {
        this.rb = rb;
    }

    public override void Execute(Enemy enemy)
    {
        rb.velocity = Vector2.zero;
    }
    
}