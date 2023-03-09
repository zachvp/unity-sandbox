using UnityEngine;

public class MovementSmoothed : MonoBehaviour
{
    public float distance = 32;
    public float velocity = 1;
    public float currentTime;
    public float smoothTime = 0.5f;
    public Vector3 target;
    public float step = 0.01f;
    public float rate;

    public void Awake()
    {
        target = transform.position;
        target.x += distance;
        rate = Vector3.Magnitude(target - transform.position) / smoothTime;
    }

    public void Update()
    {
        if (Vector3.SqrMagnitude(target - transform.position) > 0.01f)
        {
            var position = transform.position;
            var direction = Vector3.Normalize(target - transform.position);
            //position.x = Mathf.SmoothDamp(position.x, target.x, ref velocity, smoothTime);
            //position.x = Mathf.Lerp(position.x, target.x)
            //position = Vector3.Lerp(position, target, (currentTime / smoothTime) * Time.deltaTime);
            position += direction * rate * Time.deltaTime;

            transform.position = position;

            //currentTime += step;

            //step += step % smoothTime;
        }
        else
        {
            currentTime = 0;
        }
        
    }
}
