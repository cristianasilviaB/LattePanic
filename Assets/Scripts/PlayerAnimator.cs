using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING="IsWalking";

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake(){
        animator=GetComponent<Animator>();
        //animator.SetBool(IS_WALKING, false);
    }

    private void Update(){
        animator.SetBool(IS_WALKING, player.IsWalking());
        Debug.Log("Animator IsWalking: " + player.IsWalking()); 
    }
}
