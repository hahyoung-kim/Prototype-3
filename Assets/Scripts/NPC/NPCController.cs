using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NPCController : MonoBehaviour
{
    public GameObject notification;
    public int introNum;
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

                switch (introNum)
                {
                    case 1:
                        DialogueController.instance.NewDialogueInstance("Welcome to the town guard trainee test, Biker.");
                        DialogueController.instance.NewDialogueInstance(
                            "My name is Punk. Your exam officer. I am here to guide you to pass the test.");
                        DialogueController.instance.NewDialogueInstance(
                            "You need to take this laser dagger, and flex your muscles in the front section.");
                        DialogueController.instance.NewDialogueInstance(
                            "(Press <color=#31edd1>J</color> to use laser dagger)");
                        PlayerController.bMelee = true;
                        break;
                    case 2:
                        DialogueController.instance.NewDialogueInstance("Well done! Biker.");
                        DialogueController.instance.NewDialogueInstance(
                            "You deserve to have a new weapon.");
                        DialogueController.instance.NewDialogueInstance(
                            "I got a pistol for you");
                        DialogueController.instance.NewDialogueInstance(
                            "(Press <color=#31edd1>K</color> to do use pistol)");
                        PlayerController.bShoot = true;
                        break;
                    case 3:
                        DialogueController.instance.NewDialogueInstance("Survive from those monster, aha.");
                        DialogueController.instance.NewDialogueInstance(
                            "Now I'll register a powerful ability for you.");
                        DialogueController.instance.NewDialogueInstance(
                            "Wanna run faster?");
                        DialogueController.instance.NewDialogueInstance(
                            "(Press <color=#31edd1>Shift</color> to dash)");
                        PlayerController.bDash = true;
                        break;
                    case 4:
                        DialogueController.instance.NewDialogueInstance("Excellent! Biker.");
                        DialogueController.instance.NewDialogueInstance("It's time to get serious.");
                        break;
                }
                
                
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