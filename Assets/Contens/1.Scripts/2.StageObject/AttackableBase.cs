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
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("SafetyArea"))
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void Die()
    {
        stageManager.Restart();
    }
}
