using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public string level;

    public int maxHP;
    public int currentHP;

    public int attack;
    public int defense;

    public Vector2 restingPosition;
    Vector2 targetPosition;

    void Start()
    {
        currentHP = maxHP;
        restingPosition = transform.position;
        targetPosition = restingPosition;
    }

    void FixedUpdate()
    {
        Vector2 currentPosition = transform.position;
        float step = 10.0f * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, step);
        if (currentPosition == targetPosition)
        {
            targetPosition = restingPosition;
        }
    }

    public bool TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);

        // check if unit died from taking this damage
        if (currentHP == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int healing)
    {
        currentHP = Mathf.Clamp(currentHP + healing, 0, maxHP);
    }

    public void SetMovementTarget(Vector2 position)
    {
        targetPosition = position;
    }
}
