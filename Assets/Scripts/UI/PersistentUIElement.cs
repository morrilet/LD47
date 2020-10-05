using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistentUIElement : MonoBehaviour
{
    [SerializeField] private RectTransform localTransform;
    [SerializeField] private Image localImage;

    [Space, Header("Effect")]
    [SerializeField] private float openDuration;
    [SerializeField] private float closeDuration;
    [SerializeField] private AnimationCurve heightCurveIn;
    [SerializeField] private AnimationCurve heightCurveOut;
    [SerializeField] private AnimationCurve transparencyCurveIn;
    [SerializeField] private AnimationCurve transparencyCurveOut;

    private float timer = 0.0f;
    private Vector3 tempVector;
    private Color tempColor;
    private bool isOpen;

    private void Start() {
        localImage.enabled = false;
    }

    private void Update() {
        UpdateHeight();
        UpdateTransparency();

        if (isOpen && timer <= openDuration)
            timer += Time.deltaTime;
        else if (!isOpen && timer <= closeDuration)
            timer += Time.deltaTime;

        // Hide the image between uses.
        if (!isOpen && timer > closeDuration)
            localImage.enabled = false;
    }

    public void Open()
    {
        isOpen = true;
        localImage.enabled = true;
        timer = 0.0f;
    }

    public void Close() {
        isOpen = false;
        timer = 0.0f;
    }

    private void UpdateHeight() {
        if (isOpen) {
            tempVector = localTransform.anchoredPosition;
            tempVector.y = heightCurveIn.Evaluate(timer / openDuration);
            localTransform.anchoredPosition = tempVector;
        } else {
            tempVector = localTransform.anchoredPosition;
            tempVector.y = heightCurveOut.Evaluate(timer / closeDuration);
            localTransform.anchoredPosition = tempVector;
        }
    }

    private void UpdateTransparency() {
        if (isOpen) {
            tempColor = localImage.color;
            tempColor.a = transparencyCurveIn.Evaluate(timer / openDuration);
            localImage.color = tempColor;
        } else {
            tempColor = localImage.color;
            tempColor.a = transparencyCurveOut.Evaluate(timer / closeDuration);
            localImage.color = tempColor;
        }
    }
}
