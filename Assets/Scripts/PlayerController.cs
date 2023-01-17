using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerMoveSpeed;
    public Rigidbody2D PlayerRigidBody;

    private Vector2 mTranslation;
    private Vector2 mMoveDirection;
    private float mHorizontalInput;
    private float mVerticalInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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

    private void FixedUpdate()
    {
        // Move the object in Fixed Update so that it moves at a consistent rate
        PlayerRigidBody.MovePosition(mTranslation);
    }
}
