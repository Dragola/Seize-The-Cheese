using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBunnyController : MonoBehaviour
{
    public Animator animator;
    public GameObject moveToObject, idleObject;
    private Vector3 startLoc, moveToLoc, idleLoc;
    private float idleCount;
    public float idleDuration, moveSpeed;
    private bool turnFinished, movingLeft, didIdle, dead;

    public enum States
    {
        EnterIdle, Idle, EnterRight, Right, EnterLeft, Left, EnterTurning, Turning, EnterDying, Dying, EnterDead, Dead
    }
    public States State;
    // Start is called before the first frame update
    void Start()
    {
        startLoc = this.transform.position;
        moveToLoc = moveToObject.transform.position;
        idleLoc = idleObject.transform.position;
        moveToObject.GetComponent<MeshRenderer>().enabled = false;
        idleObject.GetComponent<MeshRenderer>().enabled = false;
        State = States.EnterIdle;
    }

    // Update is called once per frame
    void Update()
    {

        switch (State)
        {
            case States.EnterIdle:
                SetAnimator("isIdle");
                State = States.Idle;
                break;
            case States.Idle:
                Idle(movingLeft);
                break;
            case States.EnterRight:
                SetAnimator("isWalking");
                State = States.Right;
                break;
            case States.Right:
                Move(moveToLoc);
                break;
            case States.EnterLeft:
                SetAnimator("isWalking");
                State = States.Left;
                break;
            case States.Left:
                Move(startLoc);
                break;
            case States.EnterTurning:
                SetAnimator("isTurning");
                State = States.Turning;
                break;
            case States.Turning:
                break;
            case States.EnterDying:
                SetAnimator("isDying");
                State = States.Dying;
                break;
            case States.Dying:
                break;
            case States.EnterDead:
                SetAnimator("none");
                State = States.Dying;
                break;
            case States.Dead:
                dead = true;
                break;
            default:
                break;
        }


    }

    void SetAnimator(string newAnimation)
    {
        animator.SetBool("isTurning", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isDying", false);
        if (newAnimation == "none") return;
        animator.SetBool(newAnimation, true);
    }

    void Idle(bool WalkingLeft)
    {
        idleCount += Time.deltaTime;
        if(idleCount > idleDuration)
        {
            if (WalkingLeft == false)
                State = States.EnterRight;
            else
                State = States.EnterLeft;

            idleCount = 0;
        }
    }

    private void Move(Vector3 ToPosition) 
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, ToPosition, step);

        if (Vector3.Distance(transform.position, idleLoc) < 0.001f)
        {
            if(!didIdle) { State = States.EnterIdle;  }
        }

        if (Vector3.Distance(transform.position, ToPosition) < 0.001f)
        {
            movingLeft = !movingLeft;
            didIdle = false;
            State = States.EnterTurning;
        }
    }

    void TurnFinished()
    {
        if (movingLeft) State = States.EnterRight; else State = States.EnterLeft;
    }

}
