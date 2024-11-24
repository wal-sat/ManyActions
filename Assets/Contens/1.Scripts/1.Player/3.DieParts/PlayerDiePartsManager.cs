using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerDiePartsManager : MonoBehaviour
{
    [SerializeField] GameObject[] dieParts;
    [SerializeField] float SPEED;

    public void Die(Vector3 playerPosition)
    {
        foreach (var part in dieParts)
        {
            GameObject newPart = Instantiate(part, new Vector3(playerPosition.x, playerPosition.y, 1f), Quaternion.identity);
            newPart.SetActive(true);
            Rigidbody2D rb = newPart.GetComponent<Rigidbody2D>();

            float randomAngle = Random.Range(0, 180) * Mathf.Deg2Rad;
            Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
            rb.AddForce(randomDirection * SPEED, ForceMode2D.Impulse);
        }
    }
}
