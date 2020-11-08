using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Text canvasText;
    public Image canvasWindow;
    public GameObject birdee;

    private Rigidbody2D rigidBody;
    private Vector2 movementVector;
    private Animator animator;
    private bool talking;
    private bool explained;
    //private Text canvasText;
    private Queue<string> dialogue;

    void Start(){
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dialogue = new Queue<string>();
        dialogue.Enqueue("Laboratory is on the east part of the town. Better get there fast.");
        talking = true;
        explained = false;
    }

    void Update()
    {
        movementVector = Vector2.zero;
        if(!talking){
            if (Input.GetKey(KeyCode.W))
                movementVector = Vector2.up * moveSpeed;
            
            if (Input.GetKey(KeyCode.S))
                movementVector = Vector2.down * moveSpeed;
            
            if (Input.GetKey(KeyCode.A))
                movementVector = Vector2.left * moveSpeed;

            if (Input.GetKey(KeyCode.D))
                movementVector = Vector2.right * moveSpeed;
                
            if (Input.GetKeyDown(KeyCode.Escape) == true) {
                Application.Quit();
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Space)){
                NextDialog();
            }
        }
    }

    void FixedUpdate(){
        if(movementVector != Vector2.zero) {
            animator.SetBool("isRunning", true);
            animator.SetFloat("inputX", movementVector.x);
            animator.SetFloat("inputY", movementVector.y);
        }
        else {
            animator.SetBool("isRunning", false);
        }
        rigidBody.MovePosition(rigidBody.position + movementVector * Time.fixedDeltaTime);
    }

    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.name == "Male02" && explained){
            talking = true;
            dialogue.Clear();
            dialogue.Enqueue("Oak: Well there is a rumor, that this town is being protected by a very strong protector for centuries. But that will be just a conspiracy theory I guess. You can ask around, but I am not giving you much chances.");
            canvasWindow.enabled = true;
            canvasText.enabled = true;
        }
        if (collision.gameObject.name == "Male02" && !explained){
            /* Destroy(collision.gameObject);
            Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            Debug.Log("OnCollisionEnter2D"); */
            talking = true;
            dialogue.Clear();
            dialogue.Enqueue("Proffesor Oak: Hello there! I have been waiting for you. You are a bit late aren't you?");
            dialogue.Enqueue("Player: Proffessor, I am sorry I overslept, but now I am here and I am ready to pick my first protector!");
            dialogue.Enqueue("Proffesor Oak: You don't have to call me professor, I haven't been attending any university. I don't know why everyone calls me that. And also I do not have any more protectors. All of them were taken out by the kids who came on time.");
            dialogue.Enqueue("Player: But that cannot be, I must have my protector! What should I do?");
            dialogue.Enqueue("Oak: Well there is a rumor, that this town is being protected by a very strong protector for centuries. But that will be just a conspiracy theory I guess. You can ask around, but I am not giving you much chances.");
            dialogue.Enqueue("Player: I will do anything to have my own protector. Thank you profess... I mean, thank you.");
            canvasWindow.enabled = true;
            canvasText.enabled = true;
            explained = true;
        }
        if (collision.gameObject.name == "Female01"){
            talking = true;
            dialogue.Clear();
            dialogue.Enqueue("Young Lady: So you are trying to find our village protector? Well I think that yesterday they said on the news that it has been playing in the tall grass next to the Gary's house. Maybe you should check that. They never lie on the TV.");
            canvasWindow.enabled = true;
            canvasText.enabled = true;
        }
        if (collision.gameObject.name == "Male01"){
            talking = true;
            dialogue.Clear();
            dialogue.Enqueue("Gary: Hmp. So you don't have your own protector? Ha, I came early this morning and I already have one. But if you wanna try to find the village protector I will help you for sure (muheheh). I saw it hiding in one of the trash cans.");
            dialogue.Enqueue("Gary: Now get lost. I have to get back to my hard training!");
            canvasWindow.enabled = true;
            canvasText.enabled = true;
        }
        if (collision.gameObject.name == "Trash01"){
            talking = true;
            dialogue.Clear();
            dialogue.Enqueue("Player: Eeewww. Its only rubbish here.");
            canvasWindow.enabled = true;
            canvasText.enabled = true;
        }
        if (collision.gameObject.name == "Trash02"){
            talking = true;
            dialogue.Clear();
            dialogue.Enqueue("Player: This one is empty. Our town has a good public service.");
            canvasWindow.enabled = true;
            canvasText.enabled = true;
        }
        if (collision.gameObject.name == "Trash03"){
            talking = true;
            dialogue.Clear();
            dialogue.Enqueue("Player: Hey, there is old newspaper!");
            dialogue.Enqueue("Newspaper: NOTICE! A wild protector has been seen sitting on a branch of our oldest tree in the village. It is supposed to protect us from enemies. Please dont feed it.");
            dialogue.Enqueue("Player: Wow! This is a hot clue. An information like this must be verified. Let's go to the tree and see on my own eyes!");
            canvasWindow.enabled = true;
            canvasText.enabled = true;
            birdee.SetActive(true);
        }
        if (collision.gameObject.name == "Birdee"){
            talking = true;
            dialogue.Clear();
            dialogue.Enqueue("Birdee: Chichiripi!");
            dialogue.Enqueue("Player: Here you are! Now Im gonna train you and be the best protector trainer on the whole word!");
            dialogue.Enqueue("Birdee: Chiripi!");
            dialogue.Enqueue("(congratulations, you have won the game. Press escape to exit)");
            canvasWindow.enabled = true;
            canvasText.enabled = true;
            Destroy(collision.gameObject);
        }
    }

    void NextDialog(){
        if(dialogue.Count != 0){
            canvasText.text = dialogue.Dequeue();
        }
        else{
            canvasText.text = "(space to continue)";
            talking = false;
            canvasWindow.enabled = false;
            canvasText.enabled = false;
        }
    }
}
