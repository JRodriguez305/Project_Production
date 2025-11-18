using UnityEngine;
using TMPro;

public class ObjectInteract : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text interactPrompt;
    public TMP_Text messageText;

    [Header("Messages")]
    public string promptMessage = "Left Click to Interact";
    public string pickupMessage = "Item Acquired!";

    [Header("Interaction Settings")]
    public float interactRange = 4f;
    public float messageDuration = 2.5f;

    [Header("Audio")]
    public AudioClip pickupSound;   // 🎵 ← drag clip here
    private AudioSource audioSource;

    private Transform playerCam;
    private bool interacted = false;
    private bool isNear = false;
    private float hideTimer = 0f;

    private float checkDelay = 0f;
    private const float CHECK_INTERVAL = 0.08f;

    void Start()
    {
        playerCam = Camera.main.transform;

        if (interactPrompt) interactPrompt.gameObject.SetActive(false);
        if (messageText) messageText.gameObject.SetActive(false);

        // Avoid physics jitter if object has collider
        if (TryGetComponent(out Collider col))
            col.isTrigger = true;

        // 🎵 AUTO-CREATE AUDIOSOURCE IF SOUND ASSIGNED
        if (pickupSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        if (!interacted)
        {
            CheckDistance();

            if (isNear && Input.GetMouseButtonDown(0))
                Pickup();
        }
        else
        {
            hideTimer -= Time.deltaTime;

            if (hideTimer <= 0f && messageText.gameObject.activeSelf)
                messageText.gameObject.SetActive(false);
        }
    }

    void CheckDistance()
    {
        checkDelay -= Time.deltaTime;
        if (checkDelay > 0f) return;

        checkDelay = CHECK_INTERVAL;

        float dist = Vector3.Distance(playerCam.position, transform.position);
        bool nowNear = dist <= interactRange;

        if (!isNear && nowNear)
            ShowPrompt();
        else if (isNear && !nowNear)
            HidePrompt();

        isNear = nowNear;
    }

    void ShowPrompt()
    {
        interactPrompt.text = promptMessage;
        interactPrompt.gameObject.SetActive(true);
    }

    void HidePrompt()
    {
        interactPrompt.gameObject.SetActive(false);
    }

    void Pickup()
    {
        interacted = true;
        HidePrompt();

        // 🟢 SHOW SECOND MESSAGE
        messageText.text = pickupMessage;
        messageText.gameObject.SetActive(true);
        hideTimer = messageDuration;

        // 🎵 PLAY SOUND
        if (pickupSound != null)
            audioSource.PlayOneShot(pickupSound);
        var pick = GetComponent<PickUpFlashlightBehaviour>();
        if (pick != null)
        {
            pick.ActiveFlashlight();
        }
        // 🟥 REMOVE VISUAL + COLLISION
        if (TryGetComponent(out Renderer r)) r.enabled = false;
        if (TryGetComponent(out Collider c)) c.enabled = false;

        // ❗ Wait long enough for message + audio
        float destroyDelay = messageDuration + (pickupSound ? pickupSound.length : 0.25f);
        Destroy(gameObject, destroyDelay);
    }
}
