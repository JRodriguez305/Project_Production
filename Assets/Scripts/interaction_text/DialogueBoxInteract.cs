using UnityEngine;
using TMPro;

public class DialogueInteraction : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text interactPrompt;
    public GameObject dialoguePanel;
    public TMP_Text dialogueTitleText;
    public TMP_Text dialogueBodyText;

    [Header("Prompt Settings")]
    public string promptMessage = "Left Click to Interact";

    [Header("Dialogue Text")]
    public string dialogueTitle = "Note";
    [TextArea(3, 10)]
    public string dialogueBody = "This is a dialogue message.";

    [Header("Settings")]
    public float interactRange = 4f;

    [Header("Timing")]
    public float panelActiveTime = 3f;   // how long panel stays open

    private float panelTimer = 0f;
    private bool dialogueOpen = false;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        SetPromptAlpha(0f);
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialogueOpen)
        {
            panelTimer += Time.deltaTime;

            if (panelTimer >= panelActiveTime)
                CloseDialogue();

            return;
        }

        bool looking = IsLookingAtObject();
        interactPrompt.text = promptMessage;

        // fade prompt
        float target = looking ? 1f : 0f;
        float current = interactPrompt.color.a;
        float newA = Mathf.Lerp(current, target, Time.deltaTime * 12f);
        SetPromptAlpha(newA);

        if (looking && Input.GetMouseButtonDown(0))
            OpenDialogue();
    }

    bool IsLookingAtObject()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        return Physics.Raycast(ray, out RaycastHit hit, interactRange)
               && hit.collider.gameObject == gameObject;
    }

    void OpenDialogue()
    {
        dialogueOpen = true;
        panelTimer = 0f;

        SetPromptAlpha(0f);

        dialoguePanel.SetActive(true);
        dialogueTitleText.text = dialogueTitle;
        dialogueBodyText.text = dialogueBody;
    }

    void CloseDialogue()
    {
        dialogueOpen = false;
        panelTimer = 0f;

        dialoguePanel.SetActive(false);
    }

    void SetPromptAlpha(float a)
    {
        Color c = interactPrompt.color;
        c.a = a;
        interactPrompt.color = c;
    }
}
