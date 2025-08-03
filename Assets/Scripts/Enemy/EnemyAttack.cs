using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float attackDamage = 0f;
    private float attackInterval = 0f;
    private float detectionRadius = 1f;
    private Transform target;
    private GameObject currentTower;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private Movement movementScript;

    //Function for set the GameObject Data
    public void SetAttackData(int objectDamage, float objectAttackSpeed)
    {
        attackDamage = objectDamage;
        attackInterval = objectAttackSpeed;
    }

    //Get the target gameObject of movement script
    void Start()
    {
        movementScript = GetComponent<Movement>();
        target = movementScript.target;
    }

    void Update()
    {
        if (!isAttacking)
        {
            // Detect nearby towers and disable movement until the close tower is destroy
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Tower"))
                {
                    currentTower = hit.gameObject;
                    isAttacking = true;
                    movementScript.enabled = false;
                    attackTimer = attackInterval;
                    break;
                }
            }

            return;
        }

        //Resume enemy movement once the tower is destroy
        if (currentTower == null)
        {
            isAttacking = false;
            movementScript.enabled = true;
            return;
        }

        //Attack Cooldown
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;

            //Track if the tower health is empty
            Health towerHealth = currentTower.GetComponent<Health>();
            if (towerHealth != null)
            {
                //Send damage to the Health script of the tower
                towerHealth.TakeDamage(attackDamage);
                if (towerHealth.IsDead())
                {
                    currentTower = null;
                }
            }
            else
            {
                //Validation If Tower exists but has no health script
                currentTower = null;
            }
        }
    }

}
