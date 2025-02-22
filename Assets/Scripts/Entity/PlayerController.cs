using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // LayerMask로 여러 Layer 확인 가능!!!
    [SerializeField] private LayerMask canInteractLayerMask;
    public bool MiniGameMode = false;

    private Vector2 movementDirection;

    private Rigidbody2D playerRigidbody;
    private AnimationHandler animationHandler;
    private ResourceController resourceController;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        resourceController = GetComponent<ResourceController>();
    }
    private void FixedUpdate()
    {
        Movement(movementDirection);
    }
    private void Movement(Vector2 direction)
    {
        if (MiniGameMode)
        {
            playerRigidbody.velocity = direction * 5f + Vector2.right * 5f; // 미니게임에서는 오른쪽으로 계속 가면서 움직임
            return;
        }
        direction = direction * resourceController.MoveSpeed;

        playerRigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(MiniGameMode && collision.gameObject.CompareTag("obstacle"))
        {
            MiniGameSystem.Instance.OffLifeUI();
        }
    }

    private void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

    private void OnInteraction()
    {
        // 미니게임 끝나고 돌아오는 입력
        if (MiniGameMode && MiniGameSystem.Instance.IsRunning == false)
        {
            MiniGameSystem.Instance.MiniGameExit();
            MiniGameMode = false;
            return;
        }
        
        // Work 종류 수락
        if(MiniGameMode == false)
        {
            GameManager.Instance.LastPosition = transform.position;
            WorkManager.Instance.MissionStart();
        }
    }
}
