using System.Collections;
using UnityEngine;

public class PlayerJumpEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem jumpEffect;

    public void JumpEffect()
    {
        StartCoroutine(CJumpEffect());
    }
    IEnumerator CJumpEffect()
    {
        jumpEffect.Play();

        yield return new WaitForSeconds(jumpEffect.main.startLifetime.constantMax);

        jumpEffect.Stop();
    }
}
