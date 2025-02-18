using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // LayerMask�� ���� Layer Ȯ�� ����!!!
    [SerializeField] private LayerMask canInteractLayerMask;

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
        direction = direction * 3;

        playerRigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��ȣ�ۿ� ������Ʈ : canInteractLayerMask�� ���ԵǴ� ������ Ȯ��
        int isInsteract = canInteractLayerMask.value | (1 << collision.gameObject.layer);
        if (canInteractLayerMask.value == isInsteract)
        {
            Debug.Log("��ȣ�ۿ�");
        }
    }

    private void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }
}
