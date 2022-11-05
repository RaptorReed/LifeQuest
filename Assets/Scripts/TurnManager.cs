using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool isPlayerTurn = false;
    public bool isEnemyTurn = false;
    public bool isBattleFinished = false;

    GameObject rubyObject;
    RubyController rubyController;

    GameObject botObject;
    BotController botController;

    private void Awake()
    {
        rubyObject = GameObject.Find("ruby");
        rubyController = rubyObject.GetComponent<RubyController>();

        botObject = GameObject.Find("bot");
        botController = botObject.GetComponent<BotController>();
    }

    void Start()
    {
        isEnemyTurn = true;
    }


    void Update()
    {
        
    }
}
