using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour
{
    //fixedupdate记录“帧数”
    //记录键程和当前帧数,左右键按下的时间
    //顺序和按键时间播放键程

    public player player;
    Rigidbody2D myRigidBody;
    Collider2D myCollider;
    Animator animator;

    public int currentTime=0;
    int endTmie;
    int jumpNum=0;
    int controlNum = 0;
    float controlThrow = 0;

    public List<int> jumpKeyPresseTime;
    [SerializeField] public float runSpeed = 8f;
    [SerializeField] public float junpSpeed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        jumpKeyPresseTime= new List<int>();
        //player = GetComponent<player>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            endTmie = currentTime;
            jumpNum = 0;
            controlNum = 0;
            currentTime = 0;
        }
        if (player.isInControl && !player.hasRewind)
        {
            Jump();
        }

        if (controlNum != 0)
        {
            bool isMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            animator.SetBool("isMoving", isMoving);
        }

        if (!player.isRewinding)
        {
            Record();
        }
    }

    private void FixedUpdate()
    {
        currentTime++;
        if (player.isRewinding)
        {
            Rewind();
        }

        if (player.isInControl && !player.hasRewind)
        {
            Jump();
            Run();
        }
        if (!player.isRewinding)
        {
            player.controlThrows.Add(controlThrow);
        }

        if (controlThrow == 0)
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void Run()
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Defalut")))
        {
            controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");//-1 to +1
                                                                           //Debug.Log(controlThrow);

            Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
            bool isMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            animator.SetBool("isMoving", isMoving);
        }
    }

    private void Jump()
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Platform", "Untagged", "HorizontalPlatform")))
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityAdd = new Vector2(0f, junpSpeed);
            myRigidBody.velocity = jumpVelocityAdd;
        }
    }

    private void Rewind()
    {
        if (currentTime<endTmie)
        {
            RewindJump();
        }
        else
        {
            return;
        }

        if (controlNum < player.controlThrows.Count)
        {
            Vector2 playerVelocity = new Vector2(player.controlThrows[controlNum] * runSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
            controlNum++;
            return;

        }
        else
        {
            player.isRewinding = false;
            myRigidBody.velocity = new Vector2(0, 0);
        }

    }

    private void StopRewind()
    {
        player.isRewinding = false;
    }

    private void Record()
    {
        if (player.isInControl)
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                jumpKeyPresseTime.Add(currentTime);
            }
        }

    }

    private void RewindJump()
    {
        if (jumpNum < jumpKeyPresseTime.Count)
        {
            if (currentTime == jumpKeyPresseTime[jumpNum])
            {
                jumpNum++;
                //Debug.Log("jumpNum" + jumpNum);
                if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Platform", "Untagged", "HorizontalPlatform")))
                {
                    Vector2 jumpVelocityAdd = new Vector2(0f,junpSpeed);
                    myRigidBody.velocity = jumpVelocityAdd;
                }
            }
        }
    }
}
