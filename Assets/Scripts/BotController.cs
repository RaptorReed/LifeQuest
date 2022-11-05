using TMPro;
using UnityEngine;

public class BotController : MonoBehaviour
{
    int maxHealth = 30;
    int currentHealth;

    int attack = 10;
    int defense = 5;

    public Vector2 restingPosition;
    Vector2 targetPosition;

    GameObject healthBarObject;
    UIHealthBar healthBar;
    
    GameObject hpObject;
    TextMeshProUGUI hp;

    GameObject rubyObject;
    RubyController rubyController;

    private void Awake()
    {
        healthBarObject = GameObject.Find("EnemyBar");
        healthBar = healthBarObject.GetComponent<UIHealthBar>();

        hpObject = GameObject.Find("EnemyHP");
        hp = hpObject.GetComponent<TextMeshProUGUI>();

        rubyObject = GameObject.Find("ruby");
        rubyController = rubyObject.GetComponent<RubyController>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        restingPosition = transform.position;
        targetPosition = restingPosition;
    }

    void Update()
    {
        hp.text = $"{currentHealth}/{maxHealth}";
    }

    void FixedUpdate()
    {
        Vector2 currentPosition = transform.position;
        float step = 10.0f * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, step);
        if(currentPosition == targetPosition)
        {
            targetPosition = restingPosition;
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.SetValue(currentHealth / (float)maxHealth);
    }

    public void AttackPlayer()
    {
        targetPosition = rubyController.restingPosition;
        rubyController.ChangeHealth(attack * -1);
    }
}
