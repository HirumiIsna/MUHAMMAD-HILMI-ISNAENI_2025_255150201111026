using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour //sori klo script berantakan
{
    [SerializeField] private float moveDuration;
    [SerializeField] private float gridSize;
    [SerializeField] private LayerMask obstacleLayer;

    private bool isMoving = false;
    private Vector2 moveInput;
    private Animator animator;
    public Vector2 lastDir { get; private set;}
    public MovesTimer movesTimer;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //cek user input 
        if (isMoving == false)
        {      
            System.Func<KeyCode, bool> inputFunction;
            inputFunction = Input.GetKeyDown;

            if (inputFunction(KeyCode.W) || inputFunction(KeyCode.UpArrow)) //atas
            {
                checkCollision(Vector2.up);
                lastDir = Vector2.up;
            } 
            else if (inputFunction(KeyCode.S) || inputFunction(KeyCode.DownArrow)) //bawah
            {
                checkCollision(Vector2.down);
                lastDir = Vector2.down;
            } 
            else if (inputFunction(KeyCode.A) || inputFunction(KeyCode.LeftArrow)) //kiri
            {
                checkCollision(Vector2.left);
                lastDir = Vector2.left;
            } 
            else if (inputFunction(KeyCode.D) || inputFunction(KeyCode.RightArrow)) //kanan
            {
                checkCollision(Vector2.right);
                lastDir = Vector2.right;
            }
        }
    }

    //cek collision wall
    private void checkCollision(Vector2 direction)
    {
        var targetPosition = (Vector2)transform.position + (direction * gridSize);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, gridSize, obstacleLayer);
        
        if (!hit.collider)
        {
            StartCoroutine(Move(targetPosition));
        }
        else if (hit.collider.CompareTag("Box"))
        {
            var box = hit.collider.GetComponent<BoxController>();
            if(box != null && box.checkPush(direction))
            {
                StartCoroutine(Move(targetPosition));
            }
        }
        return;

    }

    //gerak player
    private IEnumerator Move(Vector2 targetPosition)
    {
        isMoving = true;

        movesTimer.incrementMoves();

        moveInput = targetPosition - (Vector2)transform.position;
        animator.SetBool("Walking", true);
        if (moveInput.x < 0) 
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

        Vector2 startPosition = transform.position;
        Vector2 endPosition = targetPosition;

        while (Vector2.Distance(transform.position, endPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition, (gridSize / moveDuration) * Time.deltaTime);
            yield return null;
        }


        transform.position = endPosition;

        isMoving = false;
        animator.SetBool("Walking", false);
        animator.SetFloat("LastInputX", moveInput.x);
        animator.SetFloat("LastInputY", moveInput.y);
    }

    public void forceStop()
    {
        StopAllCoroutines();
        isMoving = false;
        animator.SetBool("Walking", false);
    }
}
