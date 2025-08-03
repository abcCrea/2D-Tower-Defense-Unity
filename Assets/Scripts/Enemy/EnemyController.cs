using UnityEngine;

namespace TowerDefense.Enemies
{
    [RequireComponent(typeof(Health), typeof(Movement), typeof(EnemyAttack))]

    public class EnemyController : MonoBehaviour
    {
        //Assign EnemyData on the Enemy Prefab
        public EnemyData data;

        public void Initialize(Transform destination)
        {
            //Function for set healthData values
            var health = GetComponent<Health>();
            health.SetMaxHP(data.healthData.maxHealth);

            //Function for set MovementData values
            var move = GetComponent<Movement>();
            move.SetSpeed(data.movementSpeed, destination);

            //Funciton for set AttackData values
            var attack = GetComponent<EnemyAttack>();
            attack.SetAttackData(data.attackData.damage, data.attackData.attackSpeed);

            //Begin Connection with onDeath function
            health.OnDeath += () => Destroy(gameObject);
        }
    }
}



