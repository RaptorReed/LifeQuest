using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    GameObject playerObject;
    Unit player;

    GameObject enemyObject;
    Unit enemy;

    GameObject playerHUDObject;
    UnitHUD playerHUD;

    GameObject enemyHUDObject;
    UnitHUD enemyHUD;

    public BattleState state;

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

    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        // have battle message display that says 'start battle'
        print("start battle");

        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy);

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        //battle message 'enemy attacks'
        print("enemy attacks");
        enemy.SetMovementTarget(player.restingPosition);

        yield return new WaitForSeconds(1f);

        bool playerDied = player.TakeDamage(enemy.attack);
        playerHUD.SetHP(player);

        yield return new WaitForSeconds(1f);

        if(playerDied)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void PlayerTurn()
    {
        // battle message 'your turn'
        print("your turn");

        // light up action buttons (they should be dimmed/greyed out when not player turn)
    }

    public void OnAttackButtonPress()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerAttack());
        }
    }

    public void OnHealButtonPress()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerHeal());
        }
    }

    IEnumerator PlayerAttack()
    {
        //battle message like 'attack' or 'slash' or 'stab'
        print("stab");
        player.SetMovementTarget(enemy.restingPosition);

        yield return new WaitForSeconds(1f);

        bool enemyDied = enemy.TakeDamage(player.attack);
        enemyHUD.SetHP(enemy);

        yield return new WaitForSeconds(1f);

        if (enemyDied)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        //battle message '++{healAmt}HP'
        print($"++ {10}HP");
        player.Heal(10);
        playerHUD.SetHP(player);

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    void EndBattle()
    {
        // display battle over dialogue box
        print("battle over");

        if(state == BattleState.WON)
        {
            //set battle over text to 'you won'
            print("you won");
        }
        else if(state == BattleState.LOST)
        {
            //set battle over text to 'you lost'
            print("you lost");
        }
    }
}
