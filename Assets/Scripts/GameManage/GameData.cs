using UnityEngine;

//Table Data for create a new GameData variant
[CreateAssetMenu(menuName = "TD/GameData")]
public class GameData : ScriptableObject
{
    public int currency = 0;
    public int currencyPerTick = 0;
    public float tickInterval = 0f;
    public float spawnRate = 0f;
}
