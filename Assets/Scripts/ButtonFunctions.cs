using UnityEngine;
using UnityEngine.EventSystems; // 1

public class ButtonFunctions : MonoBehaviour
{
    int n;
    int m;
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
    
    public void ChangePlayerHP()
    {
        botController.AttackPlayer();
    }

    public void ChangeEnemyHP()
    {
        m++;
        botController.ChangeHealth(m * -1);
    }
}