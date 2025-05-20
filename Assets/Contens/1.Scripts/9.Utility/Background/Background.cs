using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] BackgroundManager backgroundManager;
    [SerializeField] GameObject virtualCameraFollowing;
    [SerializeField] GameObject[] backgroundBar;

    private Vector3[] initBarPosition;

    private void Awake()
    {
        if (backgroundManager != null) backgroundManager.Register(this);

        initBarPosition = new Vector3[ backgroundBar.Length ];

        for (int i = 0; i < backgroundBar.Length; i++)
        {
            initBarPosition[i] = backgroundBar[i].transform.position;
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < backgroundBar.Length; i++)
        {
            backgroundBar[i].transform.position = 
                new Vector3(initBarPosition[i].x + virtualCameraFollowing.transform.position.x, virtualCameraFollowing.transform.position.y, initBarPosition[i].z);
        }
    }
}
