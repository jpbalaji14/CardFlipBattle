using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CoinToss : MonoBehaviour
{
    public bool isHeadsSelected;
    public bool isTailsSelected;
    public GameObject tossButtonsGameObject;
    public GameObject tossPlaneGameObject;
    public TextMeshProUGUI resultText;
    public GameObject resultGameObject;
    public GameObject tossHeaderText;
    public int randTossNum;
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
        tossHeaderText.SetActive(false);
        randTossNum = Random.Range(0, 2);
        if(randTossNum == 0)
        {
            Debug.Log("Game Chose Heads");
            this.GetComponent<Animator>().SetBool("Toss_Heads", true);
           
        }
        else
        {
            Debug.Log("Game Chose Tails");
            this.GetComponent<Animator>().SetBool("Toss_Tails", true);
          
        }
        StartCoroutine(CoinDisable());
    }

    IEnumerator CoinDisable()
    {
        yield return new WaitForSeconds(1.2f);
        this.GetComponent<Animator>().SetBool("Toss_Heads", false);
        this.GetComponent<Animator>().SetBool("Toss_Tails", false);
       // this.GetComponent<Animator>().Play("Toss_Idle");
        yield return new WaitForSeconds(0.2f);
        if (randTossNum == 0)
        {
            if (isHeadsSelected)
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

        yield return new WaitForSeconds(1f);


        resultGameObject.SetActive(false);
        isHeadsSelected = false;
        isTailsSelected = false;
        GameManager.Instance.healthbarGameObject.SetActive(true);
        tossPlaneGameObject.SetActive(false);
        if (GameManager.Instance.isPlayerTurn)
        {
            GameManager.Instance.isPlayerInteracting = true;
        }
        else
        {
            GameManager.Instance.isOpponentInteracting = true;
        }
      
        this.gameObject.SetActive(false);
      
    }
}
