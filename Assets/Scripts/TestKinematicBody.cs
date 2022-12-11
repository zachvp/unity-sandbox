using UnityEngine;

public class TestKinematicBody : MonoBehaviour
{
    public Rigidbody2D body;
    public Collider2D attachedCollider;
    public bool isCollided;
    public Vector2 velocity;
    public float gravity = 100;
    public short speed = 50;
    public short maxSpeed = 100;

    public void FixedUpdate()
    {
        if (!isCollided)
        {
            //velocity.x = speed;
            //velocity.y = Mathf.Max(velocity.y - gravity, velocity.y - maxSpeed);
            velocity.y -= gravity * Time.fixedDeltaTime;

            //Debug.Log($"velocity: {velocity}");

            //body.transform.Translate(velocity * Time.deltaTime, Space.World);
            body.MovePosition(body.transform.position + new Vector3(velocity.x, velocity.y) * Time.fixedDeltaTime);

            var overlaps = new Collider2D[1];
            var count = attachedCollider.OverlapCollider(new ContactFilter2D(), overlaps);
            if (count > 0)
            {
                //body.transform.Translate(-velocity * Time.deltaTime, Space.World);
                //Debug.Log("wind back body");
            }
            //body.transform.position += new Vector3(velocity.x, velocity.y);
        }
        else
        {
            velocity = Vector2.zero;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var contacts = new ContactPoint2D[8];
        collision.GetContacts(contacts);

        foreach (var contact in contacts)
        {
            Debug.DrawLine(contact.point, contact.point + Vector2.up * 2, Color.red, float.PositiveInfinity, false);
            //contact.no
        }

        //if (!isCollided)
        //{
        //    var diff = collision.collider.bounds.max.y - collision.otherCollider.bounds.min.y;
        //    body.transform.Translate(new Vector2(0, diff));
        //}

        //Debug.Log($"zvp: enter kinematic collision with: {collision.collider.gameObject.name}; dist: {contact.separation}");
        isCollided = true;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log($"zvp: exit kinematic collision with: {collision.gameObject.name}");
        isCollided = false;
    }
}
