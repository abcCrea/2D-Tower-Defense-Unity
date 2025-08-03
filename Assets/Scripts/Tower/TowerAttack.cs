using System.Data.Common;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    private GameObject projectilePrefab;
    private float projectileSpeed = 0f;
    private int projectileDamage = 0;
    private float attackSpeed = 0;
    private float shootCooldown = 0f;
    private bool isTowerPlaced = false;

    //Define Tower Data values
    public void SetAttackData(TowerData data)
    {
        projectilePrefab = data.projectilePrefab;
        projectileSpeed = data.projectileSpeed;
        projectileDamage = data.attackData.damage;
        attackSpeed = data.attackData.attackSpeed;
    }

    //Cooldown for shoot projectiles
    void Update()
    {
        if (!isTowerPlaced) return;

        shootCooldown -= Time.deltaTime;

        if (shootCooldown <= 0f)
        {
            Shoot();
            shootCooldown = 1f / attackSpeed;
        }
    }

    //Function for active the tower attack unitl is placed on a tower space
    public void Activate()
    {
        isTowerPlaced = true;
    }


    void Shoot()
    {
        if (!projectilePrefab)
        {
            Debug.LogWarning("No projectile prefab assigned to TowerData");
            return;
        }

        //Create projectile gameObject and get their values
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = newProjectile.GetComponent<Projectile>();

        //Shoot to the positive X direction
        projectile.InitDirection(Vector3.right, projectileDamage, projectileSpeed);
    }
}
