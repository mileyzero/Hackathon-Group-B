using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tips : MonoBehaviour
{
    public string[] tipsMoneyDialogue = new string[5];
    public string[] tipsPopularityDialogue = new string[5];
    public string[] tipsHappinessDialogue = new string[5];

    public string[] winDialogue = new string[3];

    public TextMeshProUGUI tipText;

    //isTip bool to check if dialogue is active
    public bool isTip;

    //tipPlayed bool to check if dialogue is playing
    public bool tipPlayed;

    // Start is called before the first frame update
    public void Awake()
    {
        isTip = false;
        tipPlayed = false;

        tipsMoneyDialogue[0] = "Did you know that having insurance can protect you from unforeseen risks and provide you with peace of mind?";
        tipsMoneyDialogue[1] = "Have you considered buying insurance?";
        tipsMoneyDialogue[2] = "Remember to keep a look out for suspicious offers so you don't risk losing money!";
        tipsMoneyDialogue[3] = "Do your research before investing!!";
        tipsMoneyDialogue[4] = "Remember to weigh the Pros and Cons before deciding on something";

        tipsPopularityDialogue[0] = "Remember to decide on something that doesn't ruin the business' image.";
        tipsPopularityDialogue[1] = "In this game, insurance acts as a buff to help you manage your losses. In real life, it will also help you manage your losses, though it does not act as a buff.";
        tipsPopularityDialogue[2] = "Remember to weigh the Pros and Cons before deciding on something.";
        tipsPopularityDialogue[3] = "You can increase popularity by networking - i.e Investing in other businesses.";
        tipsPopularityDialogue[4] = "Don't rush into things that may tarnish the business' reputation.";

        tipsHappinessDialogue[0] = "Sometimes, losing a bit of cash to make your workers happy is worth it!";
        tipsHappinessDialogue[1] = "Treat your workers right to keep them happy.";
        tipsHappinessDialogue[2] = "In this game, insurance acts as a buff to help you manage your losses. In real life, it will also help you manage your losses, though it does not act as a buff.";
        tipsHappinessDialogue[3] = "Remember to weigh the Pros and Cons before deciding on something.";
        tipsHappinessDialogue[4] = "Remember to think about your workers' happiness!";

        winDialogue[0] = "YOU WIN!!!";
        winDialogue[1] = "GOOD JOB BOSS, YOU WIN!";
        winDialogue[2] = "LETS GOOOO BOSS, YOU DID IT!!!";
    }

    public void CallTipMoney()
    {
        tipText.text = GetRandomTipMoney();
        Debug.Log(GetRandomTipMoney());
    }

    public void CallTipHappiness()
    {
        tipText.text = GetRandomTipHappiness();
        Debug.Log(GetRandomTipHappiness());
    }

    public void CallTipPopularity()
    {
        tipText.text = GetRandomTipPopularity();
        Debug.Log(GetRandomTipPopularity());
    }

    public void CallWinDialogue()
    {
        tipText.text = GetRandomWinDialogue();
        Debug.Log(GetRandomWinDialogue());
    }

    string GetRandomWinDialogue()
    {
        //chooses random dialogue in winDialogue
        //then returns which winDialogue was chosen from randomDialogue randomizer
        if (isTip == false && tipPlayed == false) 
        {
            int randomWinDialogue = Random.Range(0, winDialogue.Length);
            Debug.Log(randomWinDialogue);
            return winDialogue[randomWinDialogue];
        }
        return null;
    }

    string GetRandomTipMoney()
    {
        //chooses random dialogue in tipsMoneyDialogue
        //then returns which tipsMoneyDialogue was chosen from randomDialogue randomizer
        if (isTip == false && tipPlayed == false)
        {
            int randomTipsMoney = Random.Range(0, tipsMoneyDialogue.Length);
            Debug.Log(randomTipsMoney);
            return tipsMoneyDialogue[randomTipsMoney];
        }
        return null;
    }

    string GetRandomTipHappiness()
    {
        //chooses random dialogue in randomTipsHappiness
        //then returns which randomTipsHappiness was chosen from randomDialogue randomizer
        if (isTip == false && tipPlayed == false)
        {
            int randomTipsHappiness = Random.Range(0, tipsHappinessDialogue.Length);
            Debug.Log(randomTipsHappiness);
            return tipsHappinessDialogue[randomTipsHappiness];
        }
        return null;
    }

    string GetRandomTipPopularity()
    {
        //chooses random dialogue in randomTipsHappiness
        //then returns which randomTipsHappiness was chosen from randomDialogue randomizer
        if (isTip == false && tipPlayed == false)
        {
            int randomTipsPopularity = Random.Range(0, tipsPopularityDialogue.Length);
            Debug.Log(randomTipsPopularity);
            return tipsPopularityDialogue[randomTipsPopularity];
        }
        return null;
    }
}
