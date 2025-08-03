using UnityEngine;
using TowerDefense.Towers;

public class TowerSlot : MonoBehaviour
{
    public bool HasTower { get; private set; } = false;
    private GameObject currentTower;
    private Health towerHealth;

    //
    public void PlaceTower(GameObject tower)
    {
        tower.transform.position = gameObject.transform.position;
        HasTower = true;
        currentTower = tower;

        towerHealth = tower.GetComponent<Health>();

        //Start TowerController when tower is on slot
        if (currentTower != null)
        {
            var controller = currentTower.GetComponent<TowerController>();
            controller.Initialize();
        }

        //Connect to ClearSlot function for track when tower is destroy
        if (towerHealth != null)
        {
            towerHealth.OnDeath += ClearSlot;
        }

        //Activate tower attack when is placed
        TowerAttack attack = tower.GetComponent<TowerAttack>();
        if (attack != null)
        {
            attack.Activate();
        }
    }

    //Funciton for set a slot available again if Tower is destroyed 
    public void ClearSlot()
    {
        if (!HasTower) return;

        HasTower = false;

        if (towerHealth != null)
        {
            towerHealth.OnDeath -= ClearSlot;
            towerHealth = null;
        }

        currentTower = null;
    }

}
