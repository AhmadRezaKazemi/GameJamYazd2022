using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Vector3 inputTouch;
    public Animator animator;
    [SerializeField] private float speed = 15f;
    public bool isRunning;
    [SerializeField] private GameObject textNope, textMissionComplete, textGoodJob;
    Coroutine nope;
    private int breakCount;
    private bool isShowingTxt;

    private void Start() {
        inputTouch = this.gameObject.transform.position;
        breakCount = 0;
        isShowingTxt = false;
    }
    private void Update() {
        MovePlayer();
    }

    private void MovePlayer() {
        if (Input.GetMouseButton(0)) {
            GameObject text = textNope;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] touches = Physics2D.RaycastAll(mousePos, mousePos, 0.5f);
            for(int i = 0; i< touches.Length; i++) {
                if (touches[0].transform.CompareTag("Button")) {
                    return;
                }
                else if (touches[0].transform.CompareTag("Break")) {
                    breakCount++;
                    text = textGoodJob;
                    if (breakCount > 25) {
                        textMissionComplete.SetActive(true);
                    }
                }
            }
            if (nope != null && !isShowingTxt) {
                isShowingTxt = true;
                StopCoroutine(nope);
                nope = StartCoroutine(ShowNope(text));
            }
            isRunning = true;
            animator.SetBool("Run", true);
            inputTouch = mousePos;
            if (inputTouch.x > this.gameObject.transform.position.x)
                this.transform.localScale = new Vector3(1f, 1f, 1f);
            else
                this.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (this.transform.position.x != inputTouch.x) {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector3(inputTouch.x, this.transform.position.y, this.transform.position.z), speed * Time.deltaTime);
        }
        else {
            animator.SetBool("Run", false);
            isRunning = false;
        }
    }


    private IEnumerator ShowNope(GameObject text) {
        text.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        text.SetActive(false);
        isShowingTxt = false;
    }
}
