using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() => StartCoroutine(DelayDestroy());

    public IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
