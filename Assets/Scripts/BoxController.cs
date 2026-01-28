using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour //sori klo script berantakan
{
    [SerializeField] private float moveDuration;
    [SerializeField] private float gridSize;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Sprite winBoxSprite;
    [SerializeField] private Sprite normalBoxSprite;
    [SerializeField] private Sprite winGoalSprite;
    [SerializeField] private Sprite normalGoalSprite;
    [SerializeField] private WinCondition WinCondition;

    private bool isMoving = false;

    public bool isGoal => goalCount > 0;
    private int goalCount = 0;
    public Vector2 lastDir { get; private set;}

    //cek collision wall
    public bool checkPush(Vector2 direction)
    {
        if(isMoving)
        {
            return false;
        }

        lastDir = direction;

        var targetPosition = (Vector2)transform.position + (direction * gridSize);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, gridSize - 0.1f, obstacleLayer);

        if (!hit.collider)
        {
            StartCoroutine(Move(targetPosition));
            return true;
        }

        return false;
    }

    //gerak box
    private IEnumerator Move(Vector2 targetPosition)
    {
        isMoving = true;

        AudioManager.instance.PlaySFXpush();

        Vector2 startPosition = transform.position;
        Vector2 endPosition = targetPosition;

        while (Vector2.Distance(transform.position, endPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition, (gridSize / moveDuration) * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;

        isMoving = false;
    }

    //detect goal
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            AudioManager.instance.PlaySFXgoal();
            goalCount++;
            GetComponent<SpriteRenderer>().sprite = winBoxSprite;
            other.GetComponent<SpriteRenderer>().sprite = winGoalSprite;
            WinCondition.CheckWinCondition(); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            goalCount--;
            if (goalCount <= 0)
            {
                goalCount = 0;
                GetComponent<SpriteRenderer>().sprite = normalBoxSprite;
                other.GetComponent<SpriteRenderer>().sprite = normalGoalSprite;
            } 
            else
            {
                GetComponent<SpriteRenderer>().sprite = winBoxSprite;
                other.GetComponent<SpriteRenderer>().sprite = winGoalSprite;
            }
        }
    }

    public void forceStop()
    {
        StopAllCoroutines();
        isMoving = false;
    }
}
