using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;
[Serializable]
public class CardCombo 
{
    public Transform cardTransform;
    public Card cardScript;
    public GameObject cardGameObject;
    public string cardName;
    public float cardScore;
}
[Serializable]
public class OpponentData
{
    public GameObject opponentGameObject;
    public float opponentHealth;
    public GameObject opponentHealthBarGameobject;
    public TextMeshProUGUI opponentHealthText;
}
public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    public CardSpawner cardSpawner;
    public List<CardCombo> cardCombo= new List<CardCombo>();
    public List<GameObject> cardGameObjectList = new List<GameObject>();
    public bool isPlayerTurn;
    public int Max_opponentIndex;
    public int currentOpponentIndex;
    public bool isOpponentTurn;
    public bool isOpponentInteracting=true;
    public bool isPlayerInteracting=true;
    public int OppSelectIndex=-1;
    public int OppSelectIndex2=-1;
    public float playerHealth;
    public TextMeshProUGUI playerHealthText;
    //Opp1 Data
    //public float opponentHealth; 
    //public TextMeshProUGUI opponentHealthText;
    //Opp2 Data
    public List<OpponentData> opponentDataList;

    public GameObject gameResult;
    public GameObject playerWinText;
    public GameObject opponentWinText;
    public Transform playerAttackPoint;
    public Transform opponentAttackPoint;
    public Transform playerPosition;
    public Transform opponentPosition;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        };
    }

    private void Start()
    {
        for(int i=0; i< Max_opponentIndex; i++)
        {
            opponentDataList[i].opponentGameObject.SetActive(true);
            opponentDataList[i].opponentHealthBarGameobject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = playerHealth.ToString();
        for(int i=0; i<Max_opponentIndex; i++)
        {
            opponentDataList[i].opponentHealthText.text = opponentDataList[i].opponentHealth.ToString();
           
        }
        if(playerHealth <= 0)
        {
            gameResult.SetActive(true);
            playerHealth = 0;
            opponentWinText.SetActive(true);
            isPlayerInteracting = false;
            isOpponentInteracting = false;
        }
        //else if(opponentHealth <= 0)
        //{
        //    gameResult.SetActive(true);
        //    opponentHealth = 0;
        //    playerWinText.SetActive(true);
        //    isPlayerInteracting = false;
        //    isOpponentInteracting = false;
        //}

        if (Input.GetMouseButtonDown(0) && isPlayerTurn && isPlayerInteracting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Card cardItem = hit.collider.GetComponent<Card>(); 
                if(cardItem != null && cardCombo.Count ==0)
                {
                    cardItem.CardSelect();
                }
                else if (cardItem != null && cardCombo.Count > 0 && cardCombo[0].cardTransform != hit.collider.transform.parent)
                {
                    cardItem.CardSelect();
                }
            }
        }

        if(isOpponentTurn && isOpponentInteracting && currentOpponentIndex>0)
        {
            int randNum = Random.Range(0, cardGameObjectList.Count);
            if (OppSelectIndex == -1)
            {
                OppSelectIndex = randNum;
            }
            else
            {
                if(OppSelectIndex == randNum)
                {
                    if(randNum == cardGameObjectList.Count - 1)
                    {
                      OppSelectIndex2 = randNum-1;
                    }
                    else
                    {
                        OppSelectIndex2 = randNum+1;
                    }

                }
                else
                {
                    OppSelectIndex2 = randNum;
                }
            }
            Card cardItem = cardGameObjectList[randNum].GetComponent<Card>();
            if (cardItem != null && cardCombo.Count == 0)
            {
                cardItem.CardSelect();
            }
            else if (cardItem != null && cardCombo.Count > 0 && cardCombo[0].cardTransform != cardItem.transform.parent)
            {
                cardItem.CardSelect();
                OppSelectIndex = -1;
                OppSelectIndex2 = -1;
            }
            isOpponentInteracting = false;
        }
    }

    public void CardMatchValidation()
    {
        if(cardCombo.Count ==2) 
        {
            isPlayerInteracting = false;
            if (cardCombo[0].cardName == cardCombo[1].cardName)
            {
                StartCoroutine(cardCombo[0].cardScript.CardMatched());
                StartCoroutine(cardCombo[1].cardScript.CardMatched());

                Invoke(nameof(OnCardMatched), 1f);
                
                Invoke(nameof(OpponentInteractable), 2f);
            }
            else
            {
               cardCombo[0].cardScript.CardNotMatched();
               cardCombo[1].cardScript.CardNotMatched();
                cardCombo.Clear();
                Invoke(nameof(TurnChange), 2f);
            }
            
        }
        else if(cardCombo.Count==1 && isOpponentTurn)
        {
           Invoke(nameof(OpponentInteractable), 1f);
        }
    }

    void OnCardMatched()
    {
        if (isPlayerTurn)
        {
            //Give him choice to attack any of 3 players, then reduce the attacked player health.  NOTE::: For Now using random char to attack

 
            int OppToAttackId = Random.Range(0, Max_opponentIndex);
            opponentDataList[OppToAttackId].opponentHealth -= cardCombo[0].cardScore;
            opponentDataList[OppToAttackId].opponentHealth -= cardCombo[1].cardScore;
        }
        else    //Random Opponent Attack
        {
            int playerToAttackId = Random.Range(0, 2);
            switch (playerToAttackId)
            {
                case 0:
                    playerHealth -= cardCombo[0].cardScore;
                    playerHealth -= cardCombo[1].cardScore;
                    break;

                case 1:
                    switch (currentOpponentIndex)
                    {
                        case 1:
                            int randOpponent = Random.Range(0, 2);
                            if (randOpponent == 0)
                            {
                                opponentDataList[1].opponentHealth -= cardCombo[0].cardScore;
                                opponentDataList[1].opponentHealth -= cardCombo[1].cardScore;
                            }
                            else
                            {
                                opponentDataList[2].opponentHealth -= cardCombo[0].cardScore;
                                opponentDataList[2].opponentHealth -= cardCombo[1].cardScore;
                            }
                            break;
                            case 2:
                            int randOpponent2 = Random.Range(0, 2);
                            if (randOpponent2 == 0)
                            {
                                opponentDataList[0].opponentHealth -= cardCombo[0].cardScore;
                                opponentDataList[0].opponentHealth -= cardCombo[1].cardScore;
                            }
                            else
                            {
                                opponentDataList[2].opponentHealth -= cardCombo[0].cardScore;
                                opponentDataList[2].opponentHealth -= cardCombo[1].cardScore;
                            }
                            break;
                            case 3:
                            int randOpponent3 = Random.Range(0, 2);
                            if (randOpponent3 == 0)
                            {
                                opponentDataList[0].opponentHealth -= cardCombo[0].cardScore;
                                opponentDataList[0].opponentHealth -= cardCombo[1].cardScore;
                            }
                            else
                            {
                                opponentDataList[1].opponentHealth -= cardCombo[0].cardScore;
                                opponentDataList[1].opponentHealth -= cardCombo[1].cardScore;
                            }
                            break;
                    }
                    
                break;
            }
          
        }


        cardGameObjectList.Remove(cardCombo[0].cardGameObject);
        cardGameObjectList.Remove(cardCombo[1].cardGameObject);

        if (cardGameObjectList.Count == 0)
        {
            cardSpawner.CreateSpawnPoints();
        }
        cardCombo.Clear();
    }
    
    public void OpponentInteractable()
    {
        if (isOpponentTurn)
        {
            isOpponentInteracting = true;
        }
        else
        {
            isPlayerInteracting = true;
        }
    }
    private void TurnChange()
    {
        if (isPlayerTurn)
        {
            isPlayerTurn = false;
            isOpponentTurn = true;
            isOpponentInteracting=true;
            currentOpponentIndex++;
        }
        else if(isOpponentTurn)
        {
            if (currentOpponentIndex < Max_opponentIndex)
            {
                currentOpponentIndex++;
                isOpponentInteracting = true;
            }
            else if (currentOpponentIndex == Max_opponentIndex)
            {                                                     
                isOpponentTurn = false;
                isPlayerTurn = true;
                isPlayerInteracting = true;
            }
        }
    }

}
