using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingGenerator : MonoBehaviour
{
    [SerializeField] GameObject GeneratedObject;
    [SerializeField] float GENERATE_TIME;

    private void Start()
    {
        if (GeneratedObject == null) return;
        if (GeneratedObject.GetComponent<IAiming>() == null) return;

        InvokeRepeating( "InstantiateObject", 0.1f, GENERATE_TIME);
    }

    private void InstantiateObject()
    {
        GameObject generatedObject = Instantiate(GeneratedObject, new Vector3(this.transform.position.x, this.transform.position.y, -1f), Quaternion.identity);
        generatedObject.SetActive(true);
        generatedObject.GetComponent<IAiming>().Init();
    }
}
