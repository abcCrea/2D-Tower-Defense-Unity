using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject endPoint = null;

    private Vector3 direction;
    private float speed;
    private float damage;
  
    //Set the values of the Tower Data
    public void InitDirection(Vector3 _direction, float _damage, float _speed)
    {
        direction = _direction.normalized;
        damage = _damage;
        speed = _speed;
    }

    //Define projectile goal
    void Start()
    {
        endPoint = GameObject.Find("SpawnPoints");
    }

    void Update()
    {
        float move = speed * Time.deltaTime;
        transform.position += direction * move;

        // Collision with enemies
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (Collider2D hit in hits)
        {
            Health health = hit.GetComponent<Health>();
            if (health != null)
            {
                //Send damage to enemies that the projectile hits
                health.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }
        
        // Reached end point and destroy projectile
        if (endPoint != null && transform.position.x >= endPoint.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
