using UnityEngine;

//Health Manage script Use for any tower or enemy prefab
public class Health : MonoBehaviour
{
    private float objectHealth;

    public System.Action OnDeath;

    //Assign the hp of prefab Data
    public void SetMaxHP(float dataHealth)
    {
        objectHealth = dataHealth;
    }

    //Function for receive the damage of an Attack script
    public void TakeDamage(float damage)
    {
        objectHealth -= damage;
        if (objectHealth <= 0)
        {
            Die();
        }
    }

    //Tracking for know if my gameObject have 0 of life
    public bool IsDead() => objectHealth <= 0;

    //Send a event and destroy gameObject of this script when is Die
    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
