using UnityEngine;

//Table Data for create a new Tower variant
[CreateAssetMenu(menuName = "TD/TowerData")]
public class TowerData : ScriptableObject
{
    public HealthData healthData;
    public AttackData attackData;
    public GameObject projectilePrefab;
    public float projectileSpeed = 0f;
    public int cost = 0;

}
