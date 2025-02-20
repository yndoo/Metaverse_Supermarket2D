using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class AnimationHandler : MonoBehaviour
{
    // �ִϸ��̼� Ű��
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int IsHolding = Animator.StringToHash("IsHolding");

    public RandomFood playerFood; // �÷��̾� �Ӹ� �� ����

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        WorkManager.Instance.AnimHandlerInit(this);
    }
    public void SwitchHolding(bool on)
    {
        animator.SetBool(IsHolding, on);
    }
    public void HeadFoodOn(int spriteNum)
    {
        playerFood.gameObject.SetActive(true);
        playerFood.SetSpriteNum(spriteNum);
    }
    public void HeadFoodOn()
    {
        playerFood.gameObject.SetActive(true);
        playerFood.RandomOn();
    }
    public void HeadFoodOff()
    {
        playerFood.gameObject.SetActive(false);
    }
    public void Move(Vector2 movement)
    {
        // ������
        bool mag = movement.magnitude > 0.5f;
        animator.SetBool(IsMoving, mag);
        if (!mag) return;

        // ����
        Vector2 dir = movement.normalized;
        animator.SetFloat(MoveX, dir.x);
        animator.SetFloat(MoveY, dir.y);
    }
}
