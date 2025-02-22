using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // LayerMask�� ���� Layer Ȯ�� ����!!!
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
            playerRigidbody.velocity = direction * 5f + Vector2.right * 5f; // �̴ϰ��ӿ����� ���������� ��� ���鼭 ������
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
        // �̴ϰ��� ������ ���ƿ��� �Է�
        if (MiniGameMode && MiniGameSystem.Instance.IsRunning == false)
        {
            MiniGameSystem.Instance.MiniGameExit();
            MiniGameMode = false;
            return;
        }
        
        // Work ���� ����
        if(MiniGameMode == false)
        {
            GameManager.Instance.LastPosition = transform.position;
            WorkManager.Instance.MissionStart();
        }
    }
}
