using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerAnimationState _playerAnimationState = PlayerAnimationState.IDLE;
    private Animator _animator;
    public bool isFacingRight;

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimation(_playerAnimationState);
        UpdateState();
        FacingDirection();
    }

    void UpdateAnimation(PlayerAnimationState playerAnimationState)
    {
        for (int i = 0; i < (int) PlayerAnimationState.SHOOT; i++)
        {
            string stateName = ((PlayerAnimationState)i).ToString();
            if (playerAnimationState == (PlayerAnimationState)i)
            {
                _animator.SetBool(stateName, true);
            }
            else _animator.SetBool(stateName, false);
        }
    }

    void UpdateState()
    {
        if (_playerController.isAttacking)
        {
            _playerAnimationState = PlayerAnimationState.SHOOT;
        }
        if (_playerController.inputVector != Vector2.zero)
        {
            _playerAnimationState = PlayerAnimationState.RUN;
        }
        else _playerAnimationState = PlayerAnimationState.IDLE;
    }

    void FacingDirection()
    {
        var temp = _playerController.crosshair.transform.position - _playerController.gameObject.transform.position;
        if (temp.x > 0)
        {
            isFacingRight = true;
            this.transform.localScale = new Vector3(1,1,1);
        }

        if (temp.x < 0)
        {
            isFacingRight = false;
            this.transform.localScale = new Vector3(-1,1,1);
        }
    }

    public enum PlayerAnimationState
    {
        IDLE,
        RUN,
        SHOOT
    }
    
}
