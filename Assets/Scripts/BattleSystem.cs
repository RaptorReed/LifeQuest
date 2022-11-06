using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    GameObject battleMsgObject;
    TextMeshProUGUI msgText;

    GameObject battleOverSummaryObject;
    TextMeshProUGUI summaryText;

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

        battleMsgObject = GameObject.Find("BattleMessages");
        msgText = battleMsgObject.GetComponent<TextMeshProUGUI>();

        battleOverSummaryObject = GameObject.Find("BattleOverSummary");
        summaryText = GameObject.Find("SummaryText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ClearBattleMessage();

        summaryText.text = "";
        battleOverSummaryObject.SetActive(false);

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        SetBattleMessage("Start Battle");

        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy);

        yield return new WaitForSeconds(2f);

        ClearBattleMessage();

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        SetBattleMessage("Enemy attacks!");
        enemy.SetMovementTarget(player.restingPosition);

        yield return new WaitForSeconds(1f);

        ClearBattleMessage();

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
        SetBattleMessage("Your turn");

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
            ClearBattleMessage();
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
            ClearBattleMessage();
            StartCoroutine(PlayerHeal());
        }
    }

    IEnumerator PlayerAttack()
    {
        SetBattleMessage("stab!");
        player.SetMovementTarget(enemy.restingPosition);

        yield return new WaitForSeconds(1f);

        ClearBattleMessage();

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
        SetBattleMessage("++10HP");
        player.Heal(10);
        playerHUD.SetHP(player);

        yield return new WaitForSeconds(1f);

        ClearBattleMessage();

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            summaryText.text = "You won!";
        }
        else if(state == BattleState.LOST)
        {
            summaryText.text = "You lost..";
        }

        battleOverSummaryObject.SetActive(true);
    }

    void SetBattleMessage(string msg)
    {
        msgText.text = msg;
        battleMsgObject.SetActive(true);
    }

    void ClearBattleMessage()
    {
        msgText.text = "";
        battleMsgObject.SetActive(false);
    }
}
