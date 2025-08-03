using UnityEngine;
using System;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public GameData gameData;

    [HideInInspector] public int currency = 0;
    private int currencyPerTick = 0;
    private float tickInterval = 0f;

    //Define event for check the actual currency and use for purchase tower and update gui
    public event Action<int> OnCurrencyChanged;

    private float timer;

    //Boolean for active the currency generation
    private bool isGeneratingCurrency = true;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    //Assign the gameData values
    void Start()
    {
        currency = gameData.currency;
        currencyPerTick = gameData.currencyPerTick;
        tickInterval = gameData.tickInterval;
    }

    void Update()
    {
        if (!isGeneratingCurrency) return;

        timer += Time.deltaTime;
        if (timer >= tickInterval)
        {
            timer = 0f;
            AddCurrency(currencyPerTick);
        }
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
        OnCurrencyChanged?.Invoke(currency);
    }

    //Boolean for check if its possible purchase a Tower with the current Currency
    public bool CanAfford(int cost) => currency >= cost;

    //Function for spend the currency and invoke to TowerPurchase script
    public bool SpendCurrency(int cost)
    {
        if (!CanAfford(cost)) return false;
        currency -= cost;
        OnCurrencyChanged?.Invoke(currency);
        return true;
    }

    //Stop Currency generation on GameOver
    public void StopCurrencyGeneration()
    {
        isGeneratingCurrency = false;
    }

}
