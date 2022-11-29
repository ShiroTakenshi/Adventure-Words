using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json.Converters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // player have health, and max health
    // player have attack damage, have animation, have movement

    

    [SerializeField] private TMP_Text healthText; 
    public int health;
    // public int maxHealth = 100;
    [SerializeField] public int attackDamage;
    public int restoreHealth;
    public Transform attackPoint;
    public GameObject Enemy;


    public void AnimateAttack(){
        Vector3 OriginalPosition = transform.position;
        // move with slow to attack point
        Tweener tweener = transform.DOMove(attackPoint.position, 0.4f);
        tweener.SetEase(Ease.InOutSine);
        // give back to original position
        tweener.OnComplete(() => {
            transform.DOMove(OriginalPosition, 0.2f);
        });
        EnemyDamaged();
    }

    private void EnemyDamaged(){
        var image = Enemy.GetComponent<Image>();
        Tweener tweener = image.DOColor(Color.red, 0.05f)
        .SetLoops(10, LoopType.Yoyo).SetDelay(0.5f);
        tweener.OnComplete(() => {
            image.color = Color.red;
        });
    }

    public void TakeDamage(){
        health -= this.attackDamage;
        if(health <= 0){
            Debug.Log("Player is dead");
        }
    }
    
    public void UpdateHealth(){
        healthText.text = "" + health;
        if(health >= 10){
            health = 10;
        }
    }
}
