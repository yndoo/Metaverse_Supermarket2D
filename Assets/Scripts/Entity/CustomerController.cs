using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    private static readonly int IsFinished = Animator.StringToHash("IsFinished");

    public bool EndRequest {  get; set; }
    public bool ExitMarket { get; set; }

    private Vector3 TargetPosition = new Vector3(3.5f, 0, 0);
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        transform.position = new Vector3(3.5f, -2.5f, 0);
        EndRequest = false;
    }

    void Update()
    {
        if(EndRequest)
        {
            animator.SetBool(IsFinished, true);
            EndRequest = false;
            ExitMarket = true;
            return;
        }

        // 돌아가는 코드
        if(ExitMarket)
        {
            if (transform.position.y > -2.5f)
            {
                transform.position += Vector3.down * Time.deltaTime;
            }
            return;
        }

        if(transform.position.y < TargetPosition.y)
        {
            transform.position += Vector3.up * Time.deltaTime;
        }
        
    }
}
