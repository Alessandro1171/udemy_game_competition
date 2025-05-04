using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public List<string> dialogueLines; // fill these in Inspector or load from file
    public GameObject DialogueBubble; // assign the prefab in Inspector
    public RectTransform canvasRect; // reference to your Canvas RectTransform

    private int currentLine = 0;

    void Start()
    {
        SpawnBubble(dialogueLines[currentLine]);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentLine++;
            if (currentLine < dialogueLines.Count)
            {
                SpawnBubble(dialogueLines[currentLine]);
            }
            else
            {
                Debug.Log("Dialogue finished!");
                // Optional: trigger next scene or event
            }
        }
    }

    void SpawnBubble(string text)
    {
        GameObject newBubble = Instantiate(DialogueBubble, canvasRect);
        RectTransform bubbleRect = newBubble.GetComponent<RectTransform>();

        // Set random position inside canvas bounds
        float x = Random.Range(0f, canvasRect.rect.width - bubbleRect.rect.width);
        float y = Random.Range(0f, canvasRect.rect.height - bubbleRect.rect.height);
        bubbleRect.anchoredPosition = new Vector2(x, y);

        // Set the text
        TextMeshProUGUI textComponent = newBubble.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = text;
        }
        else
        {
            Text uiText = newBubble.GetComponentInChildren<Text>();
            if (uiText != null)
                uiText.text = text;
        }
    }
}
