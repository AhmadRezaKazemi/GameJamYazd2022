using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dlagon : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        StartCoroutine(ENNN());   
    }

private IEnumerator ENNN()
    {
        yield return new WaitForSeconds(3f);
        animator.enabled = true;
    }
}
