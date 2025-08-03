using UnityEngine;
using UnityEngine.EventSystems;

//Script for implement a Drag and Drop event
//Include this on a Button or UI element for spawn a prefab Tower
public class TowerDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public System.Action OnTowerPlaced;

    public GameObject towerPrefab;

    private GameObject dragPreview;
    private Camera mainCamera;

    void Start() => mainCamera = Camera.main;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Create a Tower gameObject on scene
        dragPreview = Instantiate(towerPrefab);

        //Disable collisions while dragging for avoid problems with enemies
        dragPreview.GetComponent<Collider>().enabled = false;
    }

    //Funciton for move the gameObject with the Mouse Position and check Raycast detection
    public void OnDrag(PointerEventData eventData)
    {
        if (dragPreview == null) return;

        Vector3 mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            dragPreview.transform.position = hit.point;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragPreview == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //Check with RayCast if the gameObject is collide with a TowerSlot element
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            TowerSlot slot = hit.collider.GetComponent<TowerSlot>();
            if (slot != null && !slot.HasTower)
            {
                //Create new gameObject on the new position
                slot.PlaceTower(Instantiate(towerPrefab, slot.transform.position, Quaternion.identity));

                //Notify that the tower is placed for purchase event
                OnTowerPlaced?.Invoke();

                //Destroy previous gameObject for avoid duplicates
                Destroy(dragPreview);
            }
            else
            {
                Destroy(dragPreview);
            }
        }
        else
        {
            Destroy(dragPreview);
        }

        dragPreview = null;
    }
}
