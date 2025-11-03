using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class HiddenObjectBehaviour : MonoBehaviour
{
    private Renderer rend;
    private Collider col;

    private bool isRevealed = false;

    public string requiredTag = "Hidden";
    public float revealDistance = 8f;

    [SerializeField] private InventoryItem itemToAdd;
    [SerializeField] private InventoryBehaviour inventory;

    public Light uvFlashlight;

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();

        if (rend != null)
        {
            Hidden();
        }
    }

    void Update()
    {
        HandleRevealLogic();
    }

    void HandleRevealLogic()
    {
        if (uvFlashlight.enabled && uvFlashlight.gameObject.activeInHierarchy)
        {
            Vector3 toObject = transform.position - uvFlashlight.transform.position;
            float distance = toObject.magnitude;
            float angle = Vector3.Angle(uvFlashlight.transform.forward, toObject);

            bool inCone = distance < revealDistance && angle < uvFlashlight.spotAngle * 0.5f;

            if (inCone)
                Reveal();
            else
                Hide();
        }
        else
        {
            Hide();
        }
    }

    void Hidden()
    {
        rend.enabled = false;
        col.enabled = false;
    }

    public void Reveal()
    {
        if (!isRevealed)
        {
            isRevealed = true;
            rend.enabled = true;
            col.enabled = true;
            gameObject.layer = LayerMask.NameToLayer("HiddenTest");
        }
    }

    public void Hide()
    {
        if (isRevealed)
        {
            isRevealed = false;
            rend.enabled = false;
            col.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("HiddenTest");
        }
    }

    private void OnMouseDown()
    {
        if (isRevealed && CompareTag(requiredTag))
        {
            Debug.Log("Picked up item");
            inventory.AddInventoryItem(itemToAdd);
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, revealDistance);
    }

    // ✅ New method added for player interaction
    public void Interact()
    {
        Debug.Log("Player interacted with hidden object.");

        // Optionally add to inventory
        if (inventory != null && itemToAdd != null)
        {
            inventory.AddInventoryItem(itemToAdd);
            Destroy(gameObject);
        }

        // Immediately destroy the object
        Destroy(gameObject);
    }
}
