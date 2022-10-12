using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    [SerializeField] private Button button;
    [SerializeField] private Transform buttonTransform;
    [SerializeField] private Image slider;
    [SerializeField] private Player player;
    [SerializeField] private GameObject breakCol;
    [SerializeField] private GameObject waterTower;
    [SerializeField] private GameObject water1, water2, water3;
    [SerializeField] private GameObject Fire;
    [SerializeField] private GameObject tree;

    [SerializeField] private float changeRateFixed;
    private float changeRate;

    private bool buttonIsInMid, buttonIsInleft;

    private float screenWidth;
    private Coroutine currentCoroutine;

    private void Start() {
        screenWidth = Screen.width;
        buttonIsInMid = true;
        button.enabled = false;
        slider.fillAmount = 0.5f;
        Vector3 temp = buttonTransform.localPosition;
        temp.x = 0;
        buttonTransform.localPosition = temp;
        player.enabled = false;
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            if (buttonIsInMid) {
                player.enabled = false;
                float x = Input.mousePosition.x - (screenWidth / 2);
                buttonIsInMid = false;
                if (currentCoroutine != null) {
                    StopCoroutine(currentCoroutine);
                }
                if (x < 0) {
                    MidToRight();
                }
                else {
                    MidToLeft();
                }
            }
        }
    }

    private void MidToLeft() {
        currentCoroutine = StartCoroutine(moveButton(buttonTransform.localPosition.x, (screenWidth / 2) * (-1)));
        buttonIsInleft = true;
        button.enabled = true;
        breakCol.SetActive(false);
    }

    private void MidToRight() {
        currentCoroutine = StartCoroutine(moveButton(buttonTransform.localPosition.x, screenWidth / 2));
        buttonIsInleft = false;
        button.enabled = true;
        breakCol.SetActive(true);
    }

    public void ClickButton() {
        if (player.isRunning) {
            return;
        }
        button.enabled = false;
        buttonIsInMid = true;
        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
        }
        if (buttonIsInleft) {
            LeftToMid();
        }
        else {
            RightToMid();
        }
    }

    private void LeftToMid() {
        currentCoroutine = StartCoroutine(moveButton(buttonTransform.localPosition.x, 0));
    }

    private void RightToMid() {
        currentCoroutine = StartCoroutine(moveButton(buttonTransform.localPosition.x, 0));
    }

    private IEnumerator moveButton(float from, float to) {
        changeRate = changeRateFixed;
        changeRate = Mathf.Abs((to - from) * changeRate / (screenWidth / 2));
        float changeRateValue = (to - from) / changeRate;
        float changeSliderValue = changeRateValue / screenWidth;
        for (float i = 0; i <= changeRate; i++) {
            Vector3 temp = buttonTransform.localPosition;
            temp.x += changeRateValue;
            slider.fillAmount += changeSliderValue;
            buttonTransform.localPosition = temp;
            yield return new WaitForSeconds(0.01f);
        }
        FinishMove(from, to);
    }

    private void FinishMove(float from, float to) {
        Vector3 final = buttonTransform.localPosition;
        final.x = to;
        buttonTransform.localPosition = final;
        if (to == 0) {
            slider.fillAmount = 0.5f;
        }
        else {
            if (to > 0) {
                slider.fillAmount = 1f;
            }
            else {
                slider.fillAmount = 0f;
            }
        }
        player.enabled = true;
    }

    public IEnumerator EndGame() {
        player.enabled = false;
        yield return new WaitForSeconds(2f);
        player.textMissionComplete.SetActive(false);
        waterTower.transform.localRotation = new Quaternion(0f, 0f, 30f, 0);
        yield return StartCoroutine(moveButton(buttonTransform.localPosition.x, 0));
        yield return StartCoroutine(moveButton(buttonTransform.localPosition.x, (screenWidth / 2) * (-1)));
        water1.SetActive(true);
        water2.SetActive(true);
        water3.SetActive(true);
        yield return new WaitForSeconds(1f);
        water1.SetActive(false);
        water2.SetActive(false);
        water3.SetActive(false);
        Fire.SetActive(false);
        tree.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("CoreScene3");
    }

}
