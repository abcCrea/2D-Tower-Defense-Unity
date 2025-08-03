using UnityEngine;

//Generic Table Data for set maxHp of a gameObject
[CreateAssetMenu(menuName = "TD/Components/HealthData")]
public class HealthData : ScriptableObject
{
    public int maxHealth = 0;
}
