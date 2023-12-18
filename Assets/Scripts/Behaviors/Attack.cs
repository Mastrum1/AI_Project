using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.iOS;

public class Attack : StateMachineBehaviour
{
    private GameObject user;
    private Player _playerManager;
    private bool _isAttacking = false;

    [SerializeField] private float _attackDelay;
    [SerializeField] private float _damage;
    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerManager = GameObject.Find("Player").GetComponent<Player>();
        user = animator.gameObject;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isAttacking == false)
        {
            _isAttacking = true;
            StartCoroutine(AttackDelay());
            _playerManager.hp -= _damage;
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1f);
    }
}
