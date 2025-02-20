using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // 애니메이션 키값
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int IsHolding = Animator.StringToHash("IsHolding");

    protected Animator animator;

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
    public void Move(Vector2 movement)
    {
        // 움직임
        bool mag = movement.magnitude > 0.5f;
        animator.SetBool(IsMoving, mag);
        if (!mag) return;

        // 방향
        Vector2 dir = movement.normalized;
        animator.SetFloat(MoveX, dir.x);
        animator.SetFloat(MoveY, dir.y);
    }
}
