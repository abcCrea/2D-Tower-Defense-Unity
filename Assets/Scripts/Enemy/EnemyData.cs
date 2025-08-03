using UnityEngine;

//Table Data for create a new Enemy variant
[CreateAssetMenu(menuName = "TD/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject prefab;

    public HealthData healthData;
    public AttackData attackData;
    public float movementSpeed = 0f;
    public float weight = 0f; 
}
