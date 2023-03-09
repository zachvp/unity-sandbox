using UnityEngine;

public class MovementSmoothed : MonoBehaviour
{
    public float distance = 32;
    public float currentTime;
    public float smoothTime = 0.5f;
    public Vector3 target;
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
            position += direction * rate * Time.deltaTime;

            transform.position = position;

            currentTime += Time.deltaTime;
        }
        else
        {
            Debug.Log($"travel time: {currentTime}");
            transform.position = target;
            currentTime = 0;
        }
        
    }
}
