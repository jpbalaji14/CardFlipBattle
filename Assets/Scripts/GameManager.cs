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

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    public CardSpawner cardSpawner;
    public List<CardCombo> cardCombo= new List<CardCombo>();
    public List<GameObject> cardGameObjectList = new List<GameObject>();
    public bool isPlayerTurn;
    public bool isOpponentTurn;
    public bool isOpponentInteracting=true;
    public bool isPlayerInteracting=true;
    public int OppSelectIndex=-1;
    public int OppSelectIndex2=-1;
    public float playerHealth;
    public float opponentHealth; 
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI opponentHealthText; 
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

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = playerHealth.ToString();
        opponentHealthText.text = opponentHealth.ToString();
        if(playerHealth <= 0)
        {
            gameResult.SetActive(true);
            playerHealth = 0;
            opponentWinText.SetActive(true);
            isPlayerInteracting = false;
            isOpponentInteracting = false;
        }
        else if(opponentHealth <= 0)
        {
            gameResult.SetActive(true);
            opponentHealth = 0;
            playerWinText.SetActive(true);
            isPlayerInteracting = false;
            isOpponentInteracting = false;
        }

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

        if(isOpponentTurn && isOpponentInteracting)
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
            opponentHealth -= cardCombo[0].cardScore;
            opponentHealth -= cardCombo[1].cardScore;
        }
        else
        {
            playerHealth -= cardCombo[0].cardScore;
            playerHealth -= cardCombo[1].cardScore;
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
        }
        else
        {
            isOpponentTurn = false;
            isPlayerTurn = true;
            isPlayerInteracting = true;
        }
    }

}
