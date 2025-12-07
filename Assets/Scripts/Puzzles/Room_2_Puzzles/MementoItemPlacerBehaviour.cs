using UnityEngine;

public class MementoItemPlacerBehaviour : MonoBehaviour
{
    [SerializeField]
    private string requiredItemName;

    [SerializeField]
    private GameObject mementoItemPrefab;

    [SerializeField]
    private Dialogue playedDialogue; // delete if dialogue is changed

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

            // Remove this if statement if dialogue is changed
            if (playedDialogue != null)
            {
                DialogueHolderBehaviour.OnSayDialogue?.Invoke(playedDialogue);
            }

            this.enabled = false;
        }
    }
}
