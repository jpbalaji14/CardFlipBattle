using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    // Start is called before the first frame update
    void Start()
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
                cardCombo[0].cardScript.CardMatched();
                cardCombo[1].cardScript.CardMatched();

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
        cardCombo[0].cardTransform.gameObject.SetActive(false);
        cardCombo[1].cardTransform.gameObject.SetActive(false);

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
