using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInstantiate : MonoBehaviour
{
    [SerializeField] GameObject Ball;
    [SerializeField] int BALL_INSTANTIATE_NUMBER;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));
            worldPosition.z = 0;

            for (int i = 0; i < BALL_INSTANTIATE_NUMBER; i++)
            {
                Instantiate(Ball, worldPosition, Quaternion.identity);
            }
        }
    }
}
