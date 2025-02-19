using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // LayerMask�� ���� Layer Ȯ�� ����!!!
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
            playerRigidbody.velocity = direction + Vector2.right * MoveSpeed; // �̴ϰ��ӿ����� ���������� ��� ���鼭 ������
            return;
        }

        playerRigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

    private void OnInteraction()
    {
        MissionManager.instance.MissionStart();
    }
}
