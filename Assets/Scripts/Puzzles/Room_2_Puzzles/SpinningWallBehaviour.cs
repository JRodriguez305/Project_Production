using UnityEngine;

public class SpinningWallBehaviour : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 200f, 0f);

    private bool isSpinning = true;

    void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}
