using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionCassetteMovement : MonoBehaviour
{
    [SerializeField] float MOVE_DISTANCE;
    [SerializeField] float MOVE_TIME;

    private void Start()
    {
        Vector3 top = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + MOVE_DISTANCE / 2, this.gameObject.transform.position.z);
        Vector3 bottom = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - MOVE_DISTANCE / 2, this.gameObject.transform.position.z);

        this.gameObject.transform.position = bottom;

        this.transform.DOMove(top, MOVE_TIME / 2)
            .SetEase(Ease.InOutSine)
            .OnComplete( () => {
                this.transform.DOMove(bottom, MOVE_TIME / 2)
                    .SetEase(Ease.InOutSine);
            } )
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(this.gameObject);
    }
}
