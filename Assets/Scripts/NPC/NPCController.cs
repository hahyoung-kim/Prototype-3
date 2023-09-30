using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NPCController : MonoBehaviour
{
    public GameObject notification;

    private bool _dialogueRange;

    private bool _haveDialogue;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_dialogueRange && !_haveDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _haveDialogue = true;
                notification.SetActive(false);
                DialogueController.instance.NewDialogueInstance("Welcome to the town guard trainee test, Biker.");
                DialogueController.instance.NewDialogueInstance(
                    "My name is Punk. Your exam officer. I am here to guide you to pass the test.");
                DialogueController.instance.NewDialogueInstance(
                    "You need to take this laser dagger, and flex your muscles in the front section.");
                DialogueController.instance.NewDialogueInstance(
                    "(Press <color=#31edd1>J</color> to do melee attack)");
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_haveDialogue)
        {
            if (other.gameObject.layer == 10)
            {
                _dialogueRange = true;
                notification.SetActive(true);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            notification.SetActive(false);
        }
    }
}