using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoppableUIElement : MonoBehaviour
{
    [SerializeField] private RectTransform localTransform;
    [SerializeField] private Image localImage;

    [Space, Header("Effect")]
    [SerializeField] private float effectDuration;
    [SerializeField] private AnimationCurve heightCurve;
    [SerializeField] private AnimationCurve transparencyCurve;

    private float timer = 0.0f;
    private Vector3 tempVector;
    private Color tempColor;

    private void Update() {
        UpdateHeight();
        UpdateTransparency();

        if (timer > effectDuration) {
            GameObject.Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
    }

    private void UpdateHeight() {
        tempVector = localTransform.anchoredPosition;
        tempVector.y = heightCurve.Evaluate(timer / effectDuration);
        localTransform.anchoredPosition = tempVector;
    }

    private void UpdateTransparency() {
        tempColor = localImage.color;
        tempColor.a = transparencyCurve.Evaluate(timer / effectDuration);
        localImage.color = tempColor;
    }
}
