using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGeneratorManager : MonoBehaviour
{
    [SerializeField] CharacterGenerator[] characterGenerators;

    private int[] _pastIndex = new int[3];

    private void Awake()
    {
        for (int i = 0; i < characterGenerators.Length; i++)
        {
            characterGenerators[i].DestroyCallBack = Instantiate;
        }
    }
    private void Start()
    {
        StartCoroutine( InstantiateStart() );
    }
    IEnumerator InstantiateStart()
    {
        _pastIndex[0] = 0;
        yield return new WaitForSeconds(0.1f);
        characterGenerators[_pastIndex[0]].InstantiateCharacter();
    }

    private void Instantiate()
    {
        List<int> ints = new List<int>();
        for (int i = 0; i < characterGenerators.Length; i++) ints.Add(i);
        for (int i = 0; i < _pastIndex.Length; i++) ints.Remove(_pastIndex[i]);

        int randomIndex = Random.Range(0, ints.Count);
        characterGenerators[ints[randomIndex]].InstantiateCharacter();
        _pastIndex[2] = _pastIndex[1];
        _pastIndex[1] = _pastIndex[0];
        _pastIndex[0] = ints[randomIndex];
    }
}
