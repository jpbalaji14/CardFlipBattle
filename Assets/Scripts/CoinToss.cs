using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CoinToss : MonoBehaviour
{
    public bool isHeadsSelected;
    public bool isTailsSelected;
    public GameObject tossButtonsGameObject;
    public TextMeshProUGUI resultText;
    public GameObject resultGameObject;
    public void HeadsSelect()
    {
        isHeadsSelected = true;
        TossSelect();
    }
    public void TailsSelect()
    {
        isTailsSelected = true;
        TossSelect();
    }
    void TossSelect()
    {
        tossButtonsGameObject.SetActive(false);

        int randNum = Random.Range(0, 2);
        if(randNum == 0)
        {
            Debug.Log("Game Chose Heads");
            this.GetComponent<Animator>().SetBool("Toss_Heads", true);
            if(isHeadsSelected)
            {
                GameManager.Instance.isPlayerTurn = true;
                GameManager.Instance.isOpponentTurn = false;
                resultText.text = "PLAYER WON";
            }
            else
            {
                GameManager.Instance.isOpponentTurn = true;
                GameManager.Instance.isPlayerTurn = false;
                resultText.text = "OPPONENT WON";
            }
        }
        else
        {
            Debug.Log("Game Chose Tails");
            this.GetComponent<Animator>().SetBool("Toss_Tails", true);
            if (isHeadsSelected)
            {
                GameManager.Instance.isOpponentTurn = true;
                GameManager.Instance.isPlayerTurn = false;
                resultText.text = "OPPONENT WON";
            }
            else
            {
                GameManager.Instance.isPlayerTurn = true;
                GameManager.Instance.isOpponentTurn = false;
                resultText.text = "PLAYER WON";
            }
        }
        resultGameObject.SetActive(true);
        Invoke("CoinDisable", 3);
    }

    void CoinDisable()
    {
        resultGameObject.SetActive(false);
        isHeadsSelected = false;
        isTailsSelected = false;
        this.GetComponent<Animator>().SetBool("Toss_Heads", false);
        this.GetComponent<Animator>().SetBool("Toss_Tails", false);
        this.GetComponent<Animator>().Play("Toss_Idle");
        GameManager.Instance.isOpponentInteracting = true;
        this.gameObject.SetActive(false);
    }
}
