using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Dagger;
    private bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public int Health = 100;
    public int Damage = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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
        StartCoroutine(ResetCooldown());
       
    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    public void OnCollisionEnter(Collision dmg)
    {
        if (dmg.gameObject.CompareTag("DamageFromEnemy"))
        {
            CanAttack = true;
            Health = Health - Damage;
            Debug.Log(Health);
         
            
        }
    }
}




