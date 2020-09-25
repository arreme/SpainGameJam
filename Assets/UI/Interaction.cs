﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour
{
    public Dialogue dialogue;
    public TextMeshProUGUI dialogueField;
    public Animator animator;
    public Animator animator2;
    public float typingDelay = 0f;

    private Queue<string> sentences;
    private bool inRange = false;

    [System.Serializable]
    public class Dialogue
    {
        public string name;
        [TextArea(3, 10)]
        public string[] sentences;
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E)) DisplayNextDialogue();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartDialogue();
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EndDialogue();
            inRange = false;
        }
    }

    public void StartDialogue()
    {
        animator.SetBool("isOpen", true);
        animator2.SetBool("isOpen", true);

        sentences.Clear();

        foreach (string s in dialogue.sentences)
        {
            sentences.Enqueue(s);
        }

        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        StopAllCoroutines();
        StartCoroutine(TypeDialog(sentences.Dequeue()));
    }

    IEnumerator TypeDialog(string sentence)
    {
        dialogueField.SetText("");
        foreach (char c in sentence.ToCharArray())
        {
            dialogueField.text += c;
            yield return new WaitForSeconds(typingDelay);
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        dialogueField.SetText("");
        animator.SetBool("isOpen", false);
        animator2.SetBool("isOpen", false);
    }
}