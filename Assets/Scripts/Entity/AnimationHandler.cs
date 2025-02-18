using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // �ִϸ��̼� Ű��
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    //private static readonly int LastDirection = Animator.StringToHash("LastDirection"); // ������ ���� (�̵�����ƴ�)
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 movement)
    {
        // ������
        animator.SetBool(IsMoving, movement.magnitude > 0.5f);

        // ����
        Vector2 dir = movement.normalized;
        animator.SetFloat(MoveX, dir.x);
        animator.SetFloat(MoveY, dir.y);
    }
}
