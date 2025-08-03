using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private GameObject gameOverUI;

    private void Awake()
    {
        Instance = this;
    }

    //Stop all the events and destroy towers and enemies on scene for his tag
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        enemySpawner.StopSpawning();
        currencyManager.StopCurrencyGeneration();

        // Destroy all towers
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject tower in towers)
        {
            Destroy(tower);
        }

        // Destroy all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    //Is called on the onClick event of ResetBtn object of the scene
    public void RetryGame()
    {
        //Restart the Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
