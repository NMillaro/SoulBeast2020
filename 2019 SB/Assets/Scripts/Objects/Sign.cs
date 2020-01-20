using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable {

    //private Animator animator;
    public Text dialogueText;
    public string dialogue;
    public bool playerUp;


    IEnumerator ShowText()
    {
        textTyping = true;

        for (int i = 0; i <= dialogue.Length; i++)
        {
            currentText = dialogue.Substring(0, i);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(delay.initialValue);
        }

        textTyping = false;
    }

    protected virtual void Update () {

        currentActiveChar = gameManager.GetComponent<GameManager>().currentActiveChar;

        //if (currentActiveChar == GameObject.FindWithTag("Player"))
        //{
        //    animator = currentActiveChar.GetComponent<Animator>();

        //    if (animator.GetFloat("moveY") == 1)

        //    {
        //        playerUp = true;
        //    }
        //    else
        //    {
        //        playerUp = false;
        //    }
        //}

        if (Input.GetButtonDown("interact1") && playerInRange && currentActiveChar == GameObject.FindWithTag("Player"))
        {
            if (dialogueBox.activeInHierarchy && !textTyping)
            {
                dialogueBox.SetActive(false);
                currentActiveChar.GetComponent<PlayerMain>().currentState = CharacterState.walk;
                //Time.timeScale = 1f;
                if (minimap != null)
                {
                    minimap.SetActive(true);
                }
            }
            else
            {
                if (!textTyping)
                {
                    if (minimap != null)
                    {
                        minimap.SetActive(false);
                    }
                    currentActiveChar.GetComponent<PlayerMain>().currentState = CharacterState.interact;
                    currentActiveChar.GetComponent<Animator>().SetBool("moving", false);
                    dialogueBox.SetActive(true);
                    StartCoroutine(ShowText());
                    //Time.timeScale = 0f;
                }
            }
        }
        
		
	}
}
