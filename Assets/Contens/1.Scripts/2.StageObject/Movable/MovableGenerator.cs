using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MovableGenerator : MonoBehaviour
{
    [SerializeField] GameObject GeneratedObject;
    [SerializeField] float GENERATE_TIME;
    [SerializeField] MovableDirection GENERATE_DIRECTION;

    [SerializeField] float GENERATE_SPEED;

    private void Start()
    {
        if (GeneratedObject == null) return;
        if (GeneratedObject.GetComponent<IMovable>() == null) return;

        InvokeRepeating( "InstantiateObject", 0.1f, GENERATE_TIME);
    }

    private void InstantiateObject()
    {
        GameObject generatedObject = Instantiate(GeneratedObject, new Vector3(this.transform.position.x, this.transform.position.y, 1f), Quaternion.identity);
        generatedObject.SetActive(true);
        generatedObject.GetComponent<IMovable>().Init(GENERATE_DIRECTION, GENERATE_SPEED);
    }
}
