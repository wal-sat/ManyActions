using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject particle;
    [SerializeField] TMP_Text text;
    [SerializeField] int DEFAULT_HP;

    private int _hp;

    private void Start()
    {
        _hp = DEFAULT_HP;
        text.text = _hp.ToString();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _hp --;
            if (_hp <= 0) Die();

            text.text = _hp.ToString();

            Destroy(other.gameObject);
        }    
    }

    private void Die()
    {
        Instantiate(particle, this.gameObject.transform.position, Quaternion.identity);

        _hp = DEFAULT_HP;
        text.text = _hp.ToString();

        // 画面のワールド座標の範囲を取得
        Vector3 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

        // スポーン範囲を計算
        float minX = screenBottomLeft.x + 2f;
        float maxX = screenTopRight.x - 2f;
        float minY = screenBottomLeft.y + 2f;
        float maxY = screenTopRight.y - 2f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);
        this.gameObject.transform.position = spawnPosition;
    }
}
