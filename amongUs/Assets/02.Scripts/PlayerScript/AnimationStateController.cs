using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    int VelocityHash;
    //int isWalkingHash;
    //int isBackWalkingHash;

    void Start()
    {
        animator = GetComponent<Animator>();

        VelocityHash = Animator.StringToHash("Velocity");

        //isWalkingHash = Animator.StringToHash("isWalking");
        //isBackWalkingHash = Animator.StringToHash("isBackWalking");
    }

    // Update is called once per frame
    void Update()
    {
        //우리가 걷지않고 플레이어가 앞으로 밀면 값을true로 변경
        //bool isWalking = animator.GetBool(isWalkingHash);
        //bool isBackWalking = animator.GetBool(isBackWalkingHash);

        //앞으로걷기
        bool forwardPressed = Input.GetKey("w");
        //뒤로걷기
        //bool backwardPressed = Input.GetKey("s");


        if (forwardPressed && velocity < 1.0f)
        {
            velocity += Time.deltaTime * acceleration; 
        }
        if (!forwardPressed && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deceleration;
        }
        if (!forwardPressed && velocity < 0.0f)
        {
            velocity = 0.0f;
        }
        animator.SetFloat(VelocityHash, velocity);


        ////w키 눌리면
        //if (!isWalking && forwardPressed)
        //{
        //    //걷기 애니메이션 활성화
        //    animator.SetBool(isWalkingHash, true);
        //}
        ////w키 눌리지 않으면
        //if (isWalking && !forwardPressed)
        //{
        //    //걷기 애니메이션 비활성화
        //    animator.SetBool(isWalkingHash, false);
        //}

        ////s키 눌리면
        //if (!isBackWalking && backwardPressed)
        //{
        //    //걷기 애니메이션 활성화
        //    animator.SetBool(isBackWalkingHash, true);
        //}
        ////s키 눌리지 않으면
        //if (isBackWalking && !backwardPressed)
        //{
        //    //걷기 애니메이션 비활성화
        //    animator.SetBool(isBackWalkingHash, false);
        //}

    }
}
