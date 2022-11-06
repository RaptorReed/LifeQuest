using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hpText;
    public Image hpMask;
    float originalHPMaskSize;

    void Start()
    {
        originalHPMaskSize = hpMask.rectTransform.rect.width;
    }

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = $"Lvl {unit.level}";
        hpText.text = $"{unit.currentHP}/{unit.maxHP}";
    }

    public void SetHP(Unit unit)
    {
        float currentHPPercentage = (float)unit.currentHP / (float)unit.maxHP;
        hpMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalHPMaskSize * currentHPPercentage);
        hpText.text = $"{unit.currentHP}/{unit.maxHP}";
    }
}
