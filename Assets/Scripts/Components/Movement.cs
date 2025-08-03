using UnityEngine;

//Movement Manage script Use for any tower or enemy prefab
public class Movement : MonoBehaviour
{
    //Reference the endPoint of gameObject movement
    [HideInInspector] public Transform target;
    private float speed = 0f;

    //Set speed value with the GameObject Data
    public void SetSpeed(float objectSpeed, Transform objectTarget)
    {
        speed = objectSpeed;
        target = objectTarget;
    }

    //Loop function for move the Object and destroy when arrive or be to close of the endPoint
    void Update()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
