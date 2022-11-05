using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        restingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x = position.x + 0.1f * horizontal;
        position.y = position.y + 0.1f * vertical;
        transform.position = position;
        hp.text = $"{currentHealth}/{maxHealth}";
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.SetValue(currentHealth / (float)maxHealth);
    }
}
