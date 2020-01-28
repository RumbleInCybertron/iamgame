using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Animator animator;
    private GameObject playerObj = null;
    public Dialog dialog;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
    private void Start()
    {
        Debug.Log("AAAAAAAAAAAA");
        if (playerObj == null)
            playerObj = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (playerObj.transform.position.x >=1.8f && playerObj.transform.position.y <=-0.5f && playerObj.transform.position.x <= 5f)
        {
            animator.SetBool("IsOpen", true);
            Debug.Log("Position correct");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Key correct");
            TriggerDialogue();
        }
        else animator.SetBool("IsOpen", false);

    }
}
