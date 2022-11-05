using TMPro;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    int maxHealth = 30;
    int currentHealth;

    int atatck = 10;
    int defense = 5;

    public Vector2 restingPosition;

    GameObject healthBarObject;
    UIHealthBar healthBar;
    
    GameObject hpObject;
    TextMeshProUGUI hp;

    private void Awake()
    {
        healthBarObject = GameObject.Find("Bar");
        healthBar = healthBarObject.GetComponent<UIHealthBar>();

        hpObject = GameObject.Find("HP");
        hp = hpObject.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        restingPosition = transform.position;
    }

    void Update()
    {
        hp.text = $"{currentHealth}/{maxHealth}";
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.SetValue(currentHealth / (float)maxHealth);
    }
}
