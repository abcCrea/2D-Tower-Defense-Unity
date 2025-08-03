using UnityEngine;

//Script for attach to a GameObject and call the GameOver event when a Enemy GameObject tagged collides 
public class EnemyEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.GameOver();
            Destroy(other.gameObject);
        }
    }
}
