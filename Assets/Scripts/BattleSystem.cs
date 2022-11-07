using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.HeroEditor.Common.CharacterScripts;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    GameObject playerObject;
    Unit player;
    Character playerCharacter;

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

    GameObject startBattleTextObject;
    ParticleSystem startBattleTextParticleSystem;

    GameObject enemyTurnTextObject;
    ParticleSystem enemyTurnTextParticleSystem;

    GameObject yourTurnTextObject;
    ParticleSystem yourTurnTextParticleSystem;

    GameObject victoryTextObject;
    ParticleSystem victoryTextParticleSystem;

    GameObject defeatTextObject;
    ParticleSystem defeatTextParticleSystem;

    public BattleState state;

    private void Awake()
    {
        playerObject = GameObject.Find("DefaultChar");
        player = playerObject.GetComponent<Unit>();
        playerCharacter = playerObject.GetComponent<Character>();

        enemyObject = GameObject.Find("Enemy");
        enemy = enemyObject.GetComponent<Unit>();

        playerHUDObject = GameObject.Find("PlayerHUD");
        playerHUD = playerHUDObject.GetComponent<UnitHUD>();

        enemyHUDObject = GameObject.Find("EnemyHUD");
        enemyHUD = enemyHUDObject.GetComponent<UnitHUD>();

        /*battleOverSummaryObject = GameObject.Find("BattleOverSummary");
        summaryText = GameObject.Find("SummaryText").GetComponent<TextMeshProUGUI>();*/

        startBattleTextObject = GameObject.Find("StartBattle");
        startBattleTextParticleSystem = startBattleTextObject.GetComponent<ParticleSystem>();

        enemyTurnTextObject = GameObject.Find("EnemyTurn");
        enemyTurnTextParticleSystem = enemyTurnTextObject.GetComponent<ParticleSystem>();

        yourTurnTextObject = GameObject.Find("YourTurn");
        yourTurnTextParticleSystem = yourTurnTextObject.GetComponent<ParticleSystem>();

        victoryTextObject = GameObject.Find("Victory");
        victoryTextParticleSystem = victoryTextObject.GetComponent<ParticleSystem>();

        defeatTextObject = GameObject.Find("Defeat");
        defeatTextParticleSystem = defeatTextObject.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        /*summaryText.text = "";
        battleOverSummaryObject.SetActive(false);*/

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        startBattleTextParticleSystem.Play();

        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy);

        yield return new WaitForSeconds(2.5f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        enemyTurnTextParticleSystem.Play();

        yield return new WaitForSeconds(.5f);

        enemy.SetMovementTarget(player.restingPosition);

        yield return new WaitForSeconds(.5f);

        playerCharacter.GetHit();

        bool playerDied = player.TakeDamage(enemy.attack);
        playerHUD.SetHP(player);

        yield return new WaitForSeconds(1f);

        if(playerDied)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void PlayerTurn()
    {
        yourTurnTextParticleSystem.Play();

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
        player.SetMovementTarget(enemy.restingPosition);

        yield return new WaitForSeconds(.25f);

        playerCharacter.Slash();

        bool enemyDied = enemy.TakeDamage(player.attack);
        enemyHUD.SetHP(enemy);

        yield return new WaitForSeconds(1f);

        if (enemyDied)
        {
            yield return new WaitForSeconds(1.5f);

            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        player.Heal(10);
        playerHUD.SetHP(player);

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EndBattle()
    {
        /*textEffectParticleSystem.Play();*/

        if(state == BattleState.WON)
        {
            playerCharacter.Victory();

            yield return new WaitForSeconds(.15f);

            victoryTextParticleSystem.Play();
        }
        else if(state == BattleState.LOST)
        {
            playerCharacter.DieBackward();

            yield return new WaitForSeconds(1f);

            defeatTextParticleSystem.Play();
        }

        /*battleOverSummaryObject.SetActive(true);*/
    }
}
