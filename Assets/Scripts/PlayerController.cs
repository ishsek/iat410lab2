using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform PlayerSpriteTransform;
    public Animator PlayerAnimator;
    public Rigidbody2D PlayerRigidBody;
    public float PlayerMoveSpeed;

    private Vector2 mTranslation;
    private Vector2 mMoveDirection;
    private float mHorizontalInput;
    private float mVerticalInput;
    private bool mPlayerMovingRight = true;
    private bool mPlayerRunning = false;

    // Start is called once when the object spawns
    private void Start()
    {
        mTranslation = PlayerRigidBody.position;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePlayerMovement();
        UpdatePlayerAnimations();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void UpdatePlayerMovement()
    {
        // Get input from the input system
        // You can see which keys this is mapped to from the Edit > Project Settings > Input Manager > Axis window
        // Note: this input system is slightly outdated but still fully functional. If you want your game to support things like 
        // controllers then I would recommend the new input action system : https://www.youtube.com/watch?v=HmXU4dZbaMw&ab_channel=BMo
        mHorizontalInput = Input.GetAxisRaw("Horizontal");
        mVerticalInput = Input.GetAxisRaw("Vertical");

        // Reset move direction variable, and set it's x and y to the input values from the input system
        mMoveDirection = Vector3.zero;
        mMoveDirection.x = mHorizontalInput;
        mMoveDirection.y = mVerticalInput;

        // Calculate the ammount the object moved.
        // Note: There are MANY ways to move objects in 2D, this is just one of them.
        mTranslation = PlayerRigidBody.position + mMoveDirection * PlayerMoveSpeed * Time.fixedDeltaTime;
    }

    private void MovePlayer()
    {
        // Move the object in Fixed Update so that it moves at a consistent rate
        PlayerRigidBody.MovePosition(mTranslation);
    }

    private void UpdatePlayerAnimations()
    {
        if (mHorizontalInput > 0.01f)
        {
            if (mPlayerMovingRight == false)
            {
                PlayerSpriteTransform.localScale = new Vector3(1, 1, 1);
                mPlayerMovingRight = true;
            }

            if (mPlayerRunning == false)
            {
                PlayerAnimator.SetTrigger("Run");
                mPlayerRunning = true;
            }
        }
        else if (mHorizontalInput < -0.01f)
        {
            if (mPlayerMovingRight == true)
            {
                PlayerSpriteTransform.localScale = new Vector3(-1, 1, 1);
                mPlayerMovingRight = false;
            }

            if (mPlayerRunning == false)
            {
                PlayerAnimator.SetTrigger("Run");
                mPlayerRunning = true;
            }
        }
        else if ((mVerticalInput > 0.01f) || (mVerticalInput < -0.01f))
        {
            if (mPlayerRunning == false)
            {
                PlayerAnimator.SetTrigger("Run");
                mPlayerRunning = true;
            }
        }
        else
        {
            if (mPlayerRunning == true)
            {
                PlayerAnimator.SetTrigger("Stop");
                mPlayerRunning = false;
            }
        }
    }
}
