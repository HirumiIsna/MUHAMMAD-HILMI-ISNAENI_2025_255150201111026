using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementLobby : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2;

    private Vector2 moveInput;
    private Animator animator;
    private Rigidbody2D rigbod;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigbod = GetComponent<Rigidbody2D>();
        switch (GameManager.instance.lastBuildIndex)
        {
            case 2: //level 1 buildindex 2
                transform.position = new Vector3(0f, -1.5f, 0f);
            break;
            case 3: //level 2
                transform.position = new Vector3(0f, 4.5f, 0f);
            break;
            case 4: //level 3
                transform.position = new Vector3(6f, 4.5f, 0f);
            break;
            case 5: //level 4
                transform.position = new Vector3(12f, 4.5f, 0f);
            break;
            case 6: //level 5
                transform.position = new Vector3(12f, -1.5f, 0f);
            break;
            case 7: //level 6
                transform.position = new Vector3(18f, -1.5f, 0f);
            break;
            default:
                transform.position = new Vector3(0f, -4f, 0f);
            break;
        };
    }
    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
    }

    private void FixedUpdate()
    {
        rigbod.MovePosition(rigbod.position + moveInput * moveSpeed * Time.fixedDeltaTime);

        //animasi
        if (moveInput != Vector2.zero)
        {
            animator.SetBool("Walking", true);
            if (moveInput.x < 0)
            {
                transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
            }
            else if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        if (other.CompareTag("Level"))
        {
            other.GetComponent<LevelInfo>().showUI();            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<LevelInfo>().hideUI();
    }

    public void buttonReturnMenu()
    {
        GameManager.instance.returnToMenu();
    }
}
