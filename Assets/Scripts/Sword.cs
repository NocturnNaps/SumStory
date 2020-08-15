using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    EnemyAI Enemy;
    BoxCollider2D Collider;
    float Damage;
    float HP;
    bool isHitting = false;

    void OnTriggerEnter2D(Collider2D other)
    {
            Collider = GetComponent<BoxCollider2D>();
            Damage = GetComponentInParent<Player>().Hitpoints;
            Enemy = other.gameObject.GetComponent<EnemyAI>();
            HP = other.gameObject.GetComponentInParent<EnemyAI>().EnemyHP;
            Collider = other.gameObject.GetComponent<BoxCollider2D>();
        if (Collider.IsTouchingLayers(LayerMask.GetMask("Sword")))
        {
            Enemy.HitTheEnemy = true;
            StartCoroutine(HitTheEnemy());
            Debug.Log("Health is " + isHitting);
        }
    }
    IEnumerator HitTheEnemy()
    {
        if (isHitting) yield break;
        isHitting = true;
        DamageEnemy();
        yield return new WaitForSeconds(0.001f);
        isHitting = false;
    }
    void DamageEnemy()
    {
        Enemy.EnemyHP = Enemy.EnemyHP - Damage;
    }
}
