using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // 애니메이션 키값
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    //private static readonly int LastDirection = Animator.StringToHash("LastDirection"); // 마지막 방향 (이동방향아님)
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 movement)
    {
        // 움직임
        animator.SetBool(IsMoving, movement.magnitude > 0.5f);

        // 방향
        Vector2 dir = movement.normalized;
        animator.SetFloat(MoveX, dir.x);
        animator.SetFloat(MoveY, dir.y);
    }
}
