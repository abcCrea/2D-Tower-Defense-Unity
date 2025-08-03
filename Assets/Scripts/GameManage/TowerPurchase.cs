using UnityEngine;
using UnityEngine.UI;

//Put this script on your Tower Button
public class TowerPurchaseUnlocker : MonoBehaviour
{
    public TowerData towerData;
    public TowerDragHandler towerDragHandler;
    public Text costText;

    private void Start()
    {
        // Display tower cost on the gui
        if (costText != null)
        {
            costText.text = "$" + towerData.cost.ToString();
        }

        // Set initial state
        towerDragHandler.enabled = false;
        towerDragHandler.OnTowerPlaced += HandleTowerPlaced;

        //Connect the listener for check currency
        CurrencyManager.Instance.OnCurrencyChanged += UpdateUnlockState;
        UpdateUnlockState(CurrencyManager.Instance.currency);
    }

    //Function for able the purchase of the gameObject Tower
    void UpdateUnlockState(int currentCurrency)
    {
        bool canAfford = currentCurrency >= towerData.cost;
        towerDragHandler.enabled = canAfford;
    }

    //Check if the tower purchase and placed was successful for desactivate the Tower Button again
    void HandleTowerPlaced()
    {
        bool success = CurrencyManager.Instance.SpendCurrency(towerData.cost);
        if (success)
        {
            towerDragHandler.enabled = false;
        }
    }


    private void OnDestroy()
    {
        //Disconnect the function for purchase Towers
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnCurrencyChanged -= UpdateUnlockState;
        }

        //Disconnect the gameObject of the Drag and Drop Listener for avoid currency or active tower buttons errors
        towerDragHandler.OnTowerPlaced -= HandleTowerPlaced;
    }
}
