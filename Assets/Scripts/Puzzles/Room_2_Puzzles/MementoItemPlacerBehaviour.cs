using UnityEngine;

public class MementoItemPlacerBehaviour : MonoBehaviour
{
    [SerializeField]
    private string requiredItemName;

    [SerializeField]
    private GameObject mementoItemPrefab;

    public InventoryBehaviour inventory;
    public PuzzleTwoManagerBehaviour puzzleManager;

    void OnMouseDown()
    {
        var items = inventory.GetInventoryItems();

        InventoryItem mementoItem = items.Find(items => items.itemName == requiredItemName);

        if (mementoItem != null)
        {
            items.Remove(mementoItem);
            inventory.OnInventoryItemChange?.Invoke();

            if (mementoItemPrefab != null)
            {
                mementoItemPrefab.SetActive(true);
            }

            if (puzzleManager != null)
            {
                puzzleManager.ItemPlaced();
            }

            this.enabled = false;
        }
    }
}
