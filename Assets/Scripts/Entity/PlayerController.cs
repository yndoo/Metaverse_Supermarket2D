using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // LayerMask로 여러 Layer 확인 가능!!!
    [SerializeField] private LayerMask canInteractLayerMask;
    public bool MiniGameMode = false;
    public float MoveSpeed = 5f;

    private Vector2 movementDirection;

    private Rigidbody2D playerRigidbody;
    private AnimationHandler animationHandler;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();  
    }
    private void FixedUpdate()
    {
        Movement(movementDirection);
    }
    private void Movement(Vector2 direction)
    {
        direction = direction * MoveSpeed;
        if (MiniGameMode)
        {
            playerRigidbody.velocity = direction + Vector2.right * MoveSpeed; // 미니게임에서는 오른쪽으로 계속 가면서 움직임
            return;
        }

        playerRigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("사망");
            MiniGameSystem.instance.OffLifeUI();
        }
    }

    private void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

    private void OnInteraction()
    {
        if (MiniGameMode)
        {
            MiniGameSystem.instance.MiniGameExit();
            MiniGameMode = false;
            return;
        }

        MissionManager.instance.MissionStart();
    }
}
