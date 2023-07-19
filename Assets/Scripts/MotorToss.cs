using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class MotorToss : MonoBehaviour
{
    public Transform target;
    public Vector2 displacement;

    private Vector2 delta;
    Rigidbody2D body;
    bool isMove;

    public void Awake()
    {
        EventBus.Register<PCInputArgs>(CommandEvent.Hook, HandleInput);
        body = GetComponent<Rigidbody2D>();
    }

    public void HandleInput(PCInputArgs args)
    {

        switch (args.type)
        {
            case CoreActionMap.Player.START:
                if (args.vBool)
                {
                    Debug.Log($"TOSS");

                    
                    //float dh = 0;
                    float g = body.gravityScale * -Physics2D.gravity.y;
                    //float dx = target.position.x - transform.position.x; // Horizontal distance
                    //float dy = target.position.y - transform.position.y; // Height difference
                    //float t = Mathf.Sqrt((2 * (dy + dh)) / g); // Time of flight
                    //float vx = dx / t; // Horizontal velocity component
                    //float vy = Mathf.Sqrt((2 * g * dy)); // Vertical velocity component
                    //Vector2 initialVelocity = new Vector2(vx, vy); // Initial velocity vector

                    // method 2
                    //float h = 1;
                    //float d = target.position.x - transform.position.x; // Horizontal distance
                    //float t = Mathf.Sqrt((2 * h) / g);  // Time of flight
                    //float vx = d / t;  // Horizontal velocity component
                    //float vy = g * t;  // Vertical velocity component
                    //Vector2 adjustedVelocity = new Vector2(vx, vy);  // Adjusted initial velocity

                    // exp
                    Debug.Log($"target distance: {target.position - transform.position}");

                    //rigidbody2D.velocity.Set(velocityX, velocityY);
                    //rigidbody2D.velocity = new Vector2(velocityX, velocityY);

                    //CoreUtilities.PostFixedUpdateTask(() => { MovePos(); });
                    isMove = true;
                }
                break;
            case CoreActionMap.Player.JUMP:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
        
    }

    public void FixedUpdate()
    {
        MovePos();
        if (isMove)
        {
            //isMove = false;
        }
    }

    public void OnDestroy()
    {
        // todo: migrate to Notifications-type usgae instead of EventBus; make sure it's a singleton, NOT static
        //      Unable to use this unregister method, and also has more overhead
        //EventBus.Unregister(CommandEvent.Hook, new EventBus.);
        
    }

    public void MovePos()
    {
        delta = (displacement * Time.fixedDeltaTime);
        body.MovePosition(body.position + (displacement * delta));
        Debug.Log($"delta: {delta}");
    }
}
