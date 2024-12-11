using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UI_BarDisplay : MonoBehaviour
{

    [SerializeField] private float min = 0.0f;
    [SerializeField] private float max = 1.0f;
    [SerializeField] private float data = 0.5f;

    public bool showData;
    public bool showAsPercent;
    public bool useGradient;
    public bool useLimitViolationColors = true;

    public Gradient gradient;
    public Color underMinColor = new Color(255, 0, 0, 255);
    public Color overMaxColor = new Color(255, 0, 0, 255);

    [SerializeField] private RectTransform bgTransform;
    [SerializeField] private RectTransform fgTransform;
    [SerializeField] private Image fgImage;
    [SerializeField] private TMP_Text dataText;

    public bool testMode = false;

    public float Min { get => min; set => min = value; }
    public float Max { get => max; set => max = value; }
    public float Data
    {
        get => data;
        set
        {

            data = value;

            float roundedValue = Round(value);
            float roundedPercent = Round((value / max) * 100);

            if (value < min && useLimitViolationColors)
            {
                fgImage.color = underMinColor;
            }
            else if (value > max && useLimitViolationColors)
            {
                fgImage.color = overMaxColor;
            }
            else if (useGradient)
            {
                fgImage.color = gradient.Evaluate(value / max);
            }

            if (showData)
            {
                if (showAsPercent)
                {
                    dataText.text = $"{roundedPercent}%";
                }
                else
                {
                    dataText.text = $"{roundedValue} / {max}";
                }
            }


            if (value >= min && value <= max)
            {
                fgTransform.sizeDelta = new Vector2((value / max) * bgTransform.sizeDelta.x, bgTransform.sizeDelta.y);
            }
            else if (value > max) { fgTransform.sizeDelta = bgTransform.sizeDelta; }
            else if (value < min) { fgTransform.sizeDelta = new Vector2(0, bgTransform.sizeDelta.y); }

        }
    }

    void Start()
    {
        Assert.IsNotNull(bgTransform, "Background Transform Is Null");
        Assert.IsNotNull(fgTransform, "Foreground Transform Is Null");
        Assert.IsNotNull(dataText, "TextMeshPro Percent Text Is Null");

        dataText.enabled = showData;

    }


    void Update()
    {
        if (testMode)
        {
            Min = 0;
            Max = 100;
            Data = Mathf.Sin(Time.time) * 60 + 50;
        }
    }

    private float Round(float a)
    {
        return Mathf.Ceil(a * 100) / 100;
    }

}
