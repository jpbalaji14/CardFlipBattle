using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

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
        if (Input.GetMouseButtonDown(0))
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
    }

    public void CardMatchValidation()
    {
        if(cardCombo.Count ==2) 
        {
            if (cardCombo[0].cardName == cardCombo[1].cardName)
            {
                cardCombo[0].cardScript.CardMatched();
                cardCombo[1].cardScript.CardMatched();

                cardCombo[0].cardTransform.gameObject.SetActive(false);
                cardCombo[1].cardTransform.gameObject.SetActive(false);

                cardGameObjectList.Remove(cardCombo[0].cardGameObject);
                cardGameObjectList.Remove(cardCombo[1].cardGameObject);

                if(cardGameObjectList.Count == 0)
                {
                    cardSpawner.CreateSpawnPoints();
                }
            }
            else
            {
               cardCombo[0].cardScript.CardNotMatched();
               cardCombo[1].cardScript.CardNotMatched();
            }
            cardCombo.Clear();
        }
    }
}
