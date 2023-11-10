using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Dagger;
    private bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    private int Health = 100;
    private int Damage = 5;
    public bool isAttacking = false;
    
   
    public AudioClip DaggerSound;

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack = true)
            {
                DaggerAttack();
            }
        }
        
    }

    public void DaggerAttack()
    {
        CanAttack = false;
        //Already Attacking 
        Animator Daggeranim = Dagger.GetComponent<Animator>();
        Daggeranim.SetTrigger("Slash");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(DaggerSound);
       
       
       


        StartCoroutine(ResetCooldown());

    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
        
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("DamageFromEnemy")&& (isAttacking=true))
        { 
            CanAttack = true;
            isAttacking = true;
            Health = Health - Damage;
            Debug.Log(Health);
            
        }
    }

}
