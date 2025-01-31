﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static DatabaseManager;

public class TwoDimmentionalAnimationStateController : MonoBehaviourPunCallbacks
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    PhotonView PV;

    int VelocityZHash;
    int VelocityXHash;

    bool deathPressed;

    // Start is called before the first frame update
    void Start()
    {
        PV = photonView;
        animator = GetComponent<Animator>();
        deathPressed = false;

        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
    }

    void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool backwardPressed)
    {
        //감속과 가속 애니메이션을 담당하는 함수

        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        if (!backwardPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    void lockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool backwardPressed)
    {
        //이동 잠금과 리셋 애니메이션을 담당하는 함수
        if (forwardPressed && velocityZ < 0.5f)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (backwardPressed && velocityZ > -0.5f)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (leftPressed && velocityX > -0.5f && forwardPressed)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (rightPressed && velocityX < 0.5f && forwardPressed)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        if (!forwardPressed && !backwardPressed && velocityZ != 0.0f && (velocityZ > -0.01f && velocityZ < 0.01f))
        {
            velocityZ = 0.0f;
        }



        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.01f && velocityX < 0.01f))
        {
            velocityX = 0.0f;
        }

    }

    public void OnDeath()
    {
        deathPressed = true;
        animator.SetTrigger("Die");
    }
    public void OnKill()
    {
        animator.SetTrigger("Kill");
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine || deathPressed)
            return;

        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool backwardPressed = Input.GetKey(KeyCode.S);

        changeVelocity(forwardPressed, leftPressed, rightPressed, backwardPressed);
        lockOrResetVelocity(forwardPressed, leftPressed, rightPressed, backwardPressed);

        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }
}
