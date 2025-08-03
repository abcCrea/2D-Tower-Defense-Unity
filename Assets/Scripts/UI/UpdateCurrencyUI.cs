using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    // Assign Text element for show update Currency
    [SerializeField] private Text currencyText;

    void Start()
    {
        if (CurrencyManager.Instance != null)
        {
            // Start Listener to Currency Updates
            CurrencyManager.Instance.OnCurrencyChanged += UpdateCurrencyText;
            UpdateCurrencyText(CurrencyManager.Instance.currency); 
        }
    }

    void OnDestroy()
    {
        if (CurrencyManager.Instance != null)
             // Disconnect Listener
            CurrencyManager.Instance.OnCurrencyChanged -= UpdateCurrencyText;
    }

    //Set New Currency Text
    void UpdateCurrencyText(int newCurrency)
    {
        currencyText.text = $"Currency: {newCurrency}";
    }
}
