using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;

    public void EnterGame()
    {
        Debug.Log("enter");
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        audioSource.Play();
        animator.SetTrigger("Fire");
        yield return new WaitForSeconds(3.5f);
        Application.LoadLevel("CoreScene1");
    }
}
