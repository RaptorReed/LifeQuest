using UnityEngine;
using UnityEngine.EventSystems; // 1

public class ButtonFunctions : MonoBehaviour
{
    GameObject playerObject;
    Unit player;

    GameObject enemyObject;
    Unit enemy;

    GameObject playerHUDObject;
    UnitHUD playerHUD;

    GameObject enemyHUDObject;
    UnitHUD enemyHUD;

    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Unit>();

        enemyObject = GameObject.Find("Enemy");
        enemy = enemyObject.GetComponent<Unit>();

        playerHUDObject = GameObject.Find("PlayerHUD");
        playerHUD = playerHUDObject.GetComponent<UnitHUD>();

        enemyHUDObject = GameObject.Find("EnemyHUD");
        enemyHUD = enemyHUDObject.GetComponent<UnitHUD>();
    }
    
    public void SetUpHUDs()
    {
        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy);
    }

    public void PlayerAttack()
    {
        player.SetMovementTarget(enemy.restingPosition);
        enemy.TakeDamage(player.attack);
        enemyHUD.SetHP(enemy);
    }

    public void EnemyAttack()
    {
        enemy.SetMovementTarget(player.restingPosition);
        player.TakeDamage(enemy.attack);
        playerHUD.SetHP(player);
    }
}