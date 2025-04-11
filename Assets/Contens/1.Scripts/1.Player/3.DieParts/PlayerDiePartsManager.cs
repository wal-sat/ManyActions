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
    private int[] layerInts = new int[3];
    private int _layerIndex;
    int layerIndex
    {
        get => _layerIndex;
        set
        {
            _layerIndex = value;
            if (_layerIndex >= 3) _layerIndex = 0;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < 3; i++) layerInts[i] = LayerMask.NameToLayer("PlayerDieParts" + i);
        layerIndex = 0;
    }

    public void Die(Vector3 playerPosition)
    {
        foreach (var part in dieParts)
        {
            GameObject newPart = Instantiate(part, new Vector3(playerPosition.x, playerPosition.y, -4.5f + 0.5f * layerIndex), Quaternion.identity);
            newPart.SetActive(true);
            newPart.layer = layerInts[layerIndex++];
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
