using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    public LayerMask grassLayer;
    public LayerMask interactableLayer;

    public event Action OnEncountered;

    public Vector2 feetOffset = new Vector2(0f, -0.3f);

    private bool isMoving;
    private Vector2 input;
    private Vector2 facingDir = Vector2.down;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                Interact();

            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                input.y = 0;
            }

            if (input != Vector2.zero)
            {
                facingDir = input;

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    private void Interact()
    {
        Vector2 target = (Vector2)transform.position + feetOffset + facingDir;
        Collider2D col = Physics2D.OverlapCircle(target, 0.2f, interactableLayer);

        if (col != null)
            col.GetComponent<IInteractable>()?.Interact(this);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        Vector2 feetTarget = (Vector2)targetPos + feetOffset;

        if (Physics2D.OverlapCircle(feetTarget, 0.1f, solidObjectsLayer | interactableLayer) != null)
        {
            return false;
        }

        return true;
    }

    private void CheckForEncounters()
    {
        Vector2 feetPos = (Vector2)transform.position + feetOffset;

        if (Physics2D.OverlapCircle(feetPos, 0.2f, grassLayer) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                animator.SetBool("isMoving", false);
                OnEncountered?.Invoke();
            }
        }
    }
}
