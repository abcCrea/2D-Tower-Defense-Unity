using UnityEngine;

namespace TowerDefense.Towers
{
    [RequireComponent(typeof(Health), typeof(TowerAttack))]
    public class TowerController : MonoBehaviour
    {
        public TowerData data;

        public void Initialize()
        {
            //Function for set HealthData values
            var health = GetComponent<Health>();
            health.SetMaxHP(data.healthData.maxHealth);

            //Function for set AttackData values
            var attack = GetComponent<TowerAttack>();
            attack.SetAttackData(data);

            //Connect listener to onDeath function
            health.OnDeath += () => Destroy(gameObject);
        }
    }
}




