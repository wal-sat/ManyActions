using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInEditorOnly : MonoBehaviour
{
    [SerializeField] private bool _destroy;

    private void Start()
    {
        if (this.gameObject.activeSelf) this.gameObject.SetActive(false);

        if (_destroy) Destroy(this.gameObject);
    }
}
