using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerDiePartsManager : MonoBehaviour
{
    [SerializeField] GameObject[] dieParts;
    [SerializeField] float SPEED;
    [SerializeField] Vector2 DESTROY_DISTANCE;

    private List<GameObject> _parts = new List<GameObject>();

    public void Die(Vector3 playerPosition)
    {
        foreach (var part in dieParts)
        {
            GameObject newPart = Instantiate(part, new Vector3(playerPosition.x, playerPosition.y, -4f), Quaternion.identity);
            newPart.SetActive(true);
            newPart.GetComponent<DieParts>().Initialize( (int) DESTROY_DISTANCE.x, (int) DESTROY_DISTANCE.y, OnDestory);
            Rigidbody2D rb = newPart.GetComponent<Rigidbody2D>();

            float randomAngle = Random.Range(0, 180) * Mathf.Deg2Rad;
            Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
            rb.AddForce(randomDirection * SPEED, ForceMode2D.Impulse);

            _parts.Add(newPart);
        }
    }

    private void OnDestory(GameObject dieParts)
    {
        _parts.Remove(dieParts);
    }

    public void DestroyDieParts()
    {
        foreach (var part in _parts)
        {
            Destroy(part);
        }

        _parts.Clear();
    }
}
