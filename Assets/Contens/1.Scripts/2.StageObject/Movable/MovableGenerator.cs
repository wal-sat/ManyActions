using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableGenerator : MonoBehaviour
{
    [SerializeField] GameObject generatedObject;
    [SerializeField] float GENERATE_TIME;
    [SerializeField] MovableDirection GENERATE_DIRECTION;

    [SerializeField] float GENERATE_SPEED;

    private void Start()
    {
        if (generatedObject.GetComponent<IMovable>() == null) return;

        InvokeRepeating( "InstantiateObject", 0.1f, GENERATE_TIME);
    }

    private void InstantiateObject()
    {
        GameObject gameObject = Instantiate(generatedObject, new Vector3(this.transform.position.x, this.transform.position.y, 1f), Quaternion.identity);
        gameObject.GetComponent<IMovable>().Init(GENERATE_DIRECTION, GENERATE_SPEED);
    }
}
