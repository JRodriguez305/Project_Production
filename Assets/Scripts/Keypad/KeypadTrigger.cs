using UnityEngine;

public class KeypadTrigger : MonoBehaviour
{
    public KeypadManager keypad;
    public float interactDistance = 3f;
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!player) return;

        float dist = Vector3.Distance(player.position, transform.position);

        // Close keypad if player walks away
        if (keypad.keypadOpen && dist > interactDistance)
        {
            keypad.HideKeypad();
            return;
        }

        // Open keypad when pressing E near cube
        if (!keypad.keypadOpen && dist <= interactDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                keypad.ShowKeypad();
            }
        }
    }
}
