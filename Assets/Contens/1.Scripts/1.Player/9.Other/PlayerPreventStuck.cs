using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPreventStuck : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] int RECORD_NUMBER;
    [SerializeField] float RECORD_TIME;
    [SerializeField] float THRESHOLD;
    
    private Dictionary<GameObject, bool> _isLockPreventStuckDict = new Dictionary<GameObject, bool>();
    private List<Vector2> positions = new List<Vector2>();

    private float _timer;

    public void Initialize()
    {
        _timer = 0;

        positions.Clear();
    }
    public void PreventStuckUpdate()
    {
        if (!_isLockPreventStuckDict.Values.Any(v => v)) _timer += Time.deltaTime;

        if (_timer >= RECORD_TIME)
        {
            _timer = 0;

            positions.Add(this.gameObject.transform.position);

            if (positions.Count > RECORD_NUMBER) positions.RemoveAt(0);

            if (positions.Count == RECORD_NUMBER && IsStuckCheck(positions))
            {
                Debug.Log("スタックした");
                playerMovement.Swap();
            }
        }
    }

    private bool IsStuckCheck(List<Vector2> positions)
    {
        bool isStuck = true;

        for (int i = 0; i < positions.Count - 1; i++)
        {
            if ((positions[i] - positions[i + 1]).sqrMagnitude > THRESHOLD * THRESHOLD) isStuck = false;
        }

        return isStuck;
    }

    public void SetLockPreventStuckStatus(GameObject obj, bool isLockPreventStuck)
    {
        _isLockPreventStuckDict[obj] = isLockPreventStuck;
    }
}
