using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour
{
    [SerializeField] private LayerMask boxLayer;
    public Transform destination;
    private GameObject player;
    private bool canTeleport = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTeleport) return;

        AudioManager.instance.PlaySFXportal();

        if (other.CompareTag("Player") && canTeleport)
        {

            Movement movement = player.GetComponent<Movement>();
            if (movement == null) return;

            movement.forceStop();

            Vector2 dir = movement.lastDir;
            Vector2 exitPos = (Vector2)destination.position;
            Vector2 playerTarget = exitPos + dir * 1.0f;

            Collider2D boxCol = Physics2D.OverlapCircle(playerTarget,0.1f, boxLayer);

            if (boxCol != null)
            {
                BoxController box = boxCol.GetComponent<BoxController>();
                if (!box.checkPush(dir))
                {
                    return;
                }
            }

            player.transform.position = playerTarget;

            StartCoroutine(debounce());
        }
        else if (other.CompareTag("Box") && canTeleport)
        {
            BoxController Box = other.GetComponent<BoxController>();
            if (Box == null) return;

            Box.forceStop();

            Vector2 offset = Box.lastDir * 1.0f;

            other.transform.position = (Vector2)destination.position + offset;
            StartCoroutine(debounce());
        }
    }

    private IEnumerator debounce()
    {
        canTeleport = false;
        yield return new WaitForSeconds(0.05f);
        canTeleport = true;
    }
}
