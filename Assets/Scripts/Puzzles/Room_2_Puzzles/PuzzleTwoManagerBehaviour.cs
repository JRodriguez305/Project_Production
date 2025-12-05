using UnityEngine;

public class PuzzleTwoManagerBehaviour : MonoBehaviour
{
    public int requiredItems = 4;
    private int placedItems = 0;

    public SpinningWallBehaviour spinningWall;

    public void ItemPlaced()
    {
        placedItems++;
        Debug.Log("Item placed. Total: " + placedItems);

        if (placedItems >= requiredItems)
        {
            if (spinningWall != null)
            {
                spinningWall.StartSpinning();
            }
        }
    }
}
