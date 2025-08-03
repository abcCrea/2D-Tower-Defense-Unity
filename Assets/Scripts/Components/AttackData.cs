using UnityEngine;

//Generic Table Data for set attack values of a gameObject
[CreateAssetMenu(menuName = "TD/Components/AttackData")]
public class AttackData : ScriptableObject
{
    public int damage = 0;
    public float attackSpeed = 0f;
}
