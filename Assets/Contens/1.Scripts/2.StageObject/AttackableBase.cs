using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableBase : MonoBehaviour
{
    [SerializeField] StageManager stageManager;

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.activeSelf) Die();
        }    
    }

    public virtual void Die()
    {
        stageManager.Restart();
    }
}
