using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurst : MonoBehaviour
{
    private const float DESTROY_TIME = 3f;
    private void Start()
    {
        StartCoroutine(CDestroy());
    }

    IEnumerator CDestroy()
    {
        yield return new WaitForSecondsRealtime(DESTROY_TIME);

        Destroy(this.gameObject);
    }
}
