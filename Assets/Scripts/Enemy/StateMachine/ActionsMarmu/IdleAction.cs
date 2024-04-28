using System.Collections;
using UnityEngine;

public class IdleAction : Action
{
    private Rigidbody2D _rb;

    public IdleAction(Rigidbody2D rb)
    {
        this._rb = rb;
    }

    public override void Execute(Enemy enemy)
    {
        _rb.velocity = Vector2.zero;
    }
    
}