using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDeleyBullet : MonoBehaviour
{
    public float delay = 2.0f;
    private void Awake()
    {
        StartCoroutine("DelayedDestroy");
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
