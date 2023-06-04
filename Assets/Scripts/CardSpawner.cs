using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPosition
{
    public List<Transform> spawnPositionList;
    public Vector3 spawnHolderPosition;
}
public class CardSpawner : MonoBehaviour
{
    public bool testing;
    public GameObject cardPrefab;
    public Transform cardHolder;
    public int cardsCount;
    public List<CardData> cardDataSO = new List<CardData>();
    public List<Transform> spawnPointsList;
    private List<GameObject> selectedCards;
    private float spawnRandomValue;
    private List<GameObject> currentCardGameObjects=new List<GameObject>();   

    public List<int> randomValCard4=new List<int>(); 
    public List<int> randomValCard6 = new List<int>();
    public List<int> randomValCard8 = new List<int>();  
    public List<int> randomValCard10 = new List<int>();
    public List<int> randomValCard12 = new List<int>();
    public List<int> randomValCard16 = new List<int>();

    public Transform testSpawnArea;



    public SpawnPosition cards4;
    public SpawnPosition cards6_0;
    public SpawnPosition cards6_1;
    public SpawnPosition cards8_0;
    public SpawnPosition cards8_1; 
    public SpawnPosition cards10_0;
    public SpawnPosition cards10_1;
    public SpawnPosition cards12_0;
    public SpawnPosition cards12_1;
    public SpawnPosition cards16;
    // Start is called before the first frame update
    void Start()
    {
        CreateSpawnPoints();
    }

    [ContextMenu("SpawnCards")]
    void CreateSpawnPoints()
    {
        if (testing)
        {
            spawnRandomValue = -1;
            DeleteRandomCards();
        }
        if(spawnRandomValue == -1)
        {
            ChooseCardType();
            ClearSpawnPoints();
            float randValue = Random.Range(0, 2);
            spawnRandomValue = randValue;
        }
        else
        {
            DeleteRandomCards();
            DeleteCards();
        }

        switch (cardsCount)
        {
            case 4:
                cardHolder.position = cards4.spawnHolderPosition;
                CardMatchValidation(randomValCard4);
                CardSpawn(cards4.spawnPositionList);
                break;
            case 6:
                CardMatchValidation(randomValCard6);
                if (spawnRandomValue == 0)
                {
                    cardHolder.position = cards6_0.spawnHolderPosition;
                    CardSpawn(cards6_0.spawnPositionList);
                }
                else
                {
                    cardHolder.position = cards6_1.spawnHolderPosition;
                    CardSpawn(cards6_1.spawnPositionList);
                }
                break;
            case 8:
                CardMatchValidation(randomValCard8);
                if (spawnRandomValue == 0)
                {
                    cardHolder.position = cards8_0.spawnHolderPosition;
                    CardSpawn(cards8_0.spawnPositionList);
                }
                else
                {
                    cardHolder.position = cards8_1.spawnHolderPosition;
                    CardSpawn(cards8_1.spawnPositionList);
                }
                break;
            case 10:
                CardMatchValidation(randomValCard10);
                if (spawnRandomValue == 0)
                {
                    cardHolder.position = cards10_0.spawnHolderPosition;
                    CardSpawn(cards10_0.spawnPositionList);
                }
                else
                {
                    cardHolder.position = cards10_1.spawnHolderPosition;
                    CardSpawn(cards10_1.spawnPositionList);
                }
                break;
            case 12:
                CardMatchValidation(randomValCard12);
                if (spawnRandomValue == 0)
                {
                    cardHolder.position = cards12_0.spawnHolderPosition;
                    CardSpawn(cards12_0.spawnPositionList);
                }
                else
                {
                    cardHolder.position = cards12_1.spawnHolderPosition;
                    CardSpawn(cards12_1.spawnPositionList);
                }
                break;
            case 16:
                CardMatchValidation(randomValCard16);
                cardHolder.position = cards16.spawnHolderPosition;
                CardSpawn(cards16.spawnPositionList);
                break;
        }
    }

    [ContextMenu("Test Delete")]
    void DeleteCards()
    {
        switch (cardsCount)
        {
            case 4:
                ClearCards(cards4.spawnPositionList);
                break;
            case 6:
                if (spawnRandomValue == 0)
                {
                    ClearCards(cards6_0.spawnPositionList);
                }
                else
                {
                    ClearCards(cards6_1.spawnPositionList);
                }
                break;
            case 8:
                if (spawnRandomValue == 0)
                {
                    ClearCards(cards8_0.spawnPositionList);
                }
                else
                {
                    ClearCards(cards8_1.spawnPositionList);
                }
                break;
            case 10:
                if (spawnRandomValue == 0)
                {
                    ClearCards(cards10_0.spawnPositionList);
                }
                else
                {
                    ClearCards(cards10_1.spawnPositionList);
                }
                break;
            case 12:
                if (spawnRandomValue == 0)
                {
                    ClearCards(cards12_0.spawnPositionList);
                }
                else
                {
                    ClearCards(cards12_1.spawnPositionList);
                }
                break;
            case 16:
                ClearCards(cards16.spawnPositionList);
                break;
        }
    }
    
    void DeleteRandomCards()
    {
        randomValCard4.Clear();
        randomValCard6.Clear();
        randomValCard8.Clear();
        randomValCard10.Clear();
        randomValCard12.Clear(); 
        randomValCard16.Clear();
    }
    void ClearCards(List<Transform> spawnPositions)
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
           Destroy(spawnPositions[i].GetChild(0).gameObject);
        }
    }
    void ClearSpawnPoints()
    {

        for (int i = 0; i < spawnPointsList.Count; i++)
        {
            spawnPointsList[i].gameObject.SetActive(false);
        }
        cardHolder.position = Vector3.zero;
    }

    void CardMatchValidation(List<int> randCardNumberList)
    {
        for (int i = 0; i < cardsCount; i++)
        {
            if (i < 2)
            {
                CardCreation(randCardNumberList[0]);
            }
            else if (i >= 2 && i < 4)
            {
                CardCreation(randCardNumberList[1]);
            }
            else if (i >= 4 && i < 6)
            {
                CardCreation(randCardNumberList[2]);
            }
            else if (i >= 6 && i < 8)
            {
                CardCreation(randCardNumberList[3]);
            }
            else if (i >= 8 && i < 10)
            {
                CardCreation(randCardNumberList[4]);
            }
            else if (i >= 10 && i < 12)
            {
                CardCreation(randCardNumberList[5]);
            }
        }
    }
    void CardCreation( int randomValueList)
    {
        var card = Instantiate(cardPrefab, testSpawnArea);
        card.GetComponent<Card>().cardDataSo = cardDataSO[randomValueList]; 
        card.GetComponent<Card>().CardSetUp();
        currentCardGameObjects.Add(card);
    }  
    
    
    void CardSpawn(List<Transform> spawnPosList)
    {   
        for (int i = 0; i < spawnPosList.Count; i++)
        {
            float randomId = Random.Range(0,currentCardGameObjects.Count);
            spawnPosList[i].gameObject.SetActive(true);
            Instantiate(currentCardGameObjects[(int)randomId], spawnPosList[i]);
            currentCardGameObjects.RemoveAt((int)randomId);
        }
        if (testSpawnArea.childCount > 0)
        {
            for (int i = 0; i < testSpawnArea.childCount; i++)
            {
                Destroy(testSpawnArea.GetChild(i).gameObject);
            }
            currentCardGameObjects.Clear();
        }
    }


    void ChooseCardType()
    {
        switch (cardsCount)
        {
            case 4:
                float randomVal4_1 = Random.Range(0, cardDataSO.Count);
                randomValCard4.Add((int)randomVal4_1);
                float randomVal4_2 = Random.Range(0, cardDataSO.Count);
                if(randomVal4_2 == randomVal4_1)
                {
                    randomVal4_2 = Random.Range(0, cardDataSO.Count);
                    randomValCard4.Add((int)randomVal4_2);
                }
                else
                {
                    randomValCard4.Add((int)randomVal4_2);
                }
                break;
            case 6:
                float randomVal6_1 = Random.Range(0, cardDataSO.Count);
                float randomVal6_2 = Random.Range(0, cardDataSO.Count);
                float randomVal6_3 = Random.Range(0, cardDataSO.Count);
                randomValCard6.Add((int)randomVal6_1);
                randomValCard6.Add((int)randomVal6_2);
                randomValCard6.Add((int)randomVal6_3);

                break;
            case 8:
                float randomVal8_1 = Random.Range(0, cardDataSO.Count);
                float randomVal8_2 = Random.Range(0, cardDataSO.Count);
                float randomVal8_3 = Random.Range(0, cardDataSO.Count);
                float randomVal8_4 = Random.Range(0, cardDataSO.Count);
                randomValCard8.Add((int)randomVal8_1);
                randomValCard8.Add((int)randomVal8_2);
                randomValCard8.Add((int)randomVal8_3);
                randomValCard8.Add((int)randomVal8_4);
                break;
            case 10:
                float randomVal10_1 = Random.Range(0, cardDataSO.Count);
                float randomVal10_2 = Random.Range(0, cardDataSO.Count);
                float randomVal10_3 = Random.Range(0, cardDataSO.Count);
                float randomVal10_4 = Random.Range(0, cardDataSO.Count);
                float randomVal10_5 = Random.Range(0, cardDataSO.Count);
                randomValCard10.Add((int)randomVal10_1);
                randomValCard10.Add((int)randomVal10_2);
                randomValCard10.Add((int)randomVal10_3);
                randomValCard10.Add((int)randomVal10_4);
                randomValCard10.Add((int)randomVal10_5);
                break;
            case 12:
                float randomVal12_1 = Random.Range(0, cardDataSO.Count);
                float randomVal12_2 = Random.Range(0, cardDataSO.Count);
                float randomVal12_3 = Random.Range(0, cardDataSO.Count);
                float randomVal12_4 = Random.Range(0, cardDataSO.Count);
                float randomVal12_5 = Random.Range(0, cardDataSO.Count);
                float randomVal12_6 = Random.Range(0, cardDataSO.Count);
                randomValCard12.Add((int)randomVal12_1);
                randomValCard12.Add((int)randomVal12_2);
                randomValCard12.Add((int)randomVal12_3);
                randomValCard12.Add((int)randomVal12_4);
                randomValCard12.Add((int)randomVal12_5);
                randomValCard12.Add((int)randomVal12_6);
                break;
            case 16:
                float randomVal16_1 = Random.Range(0, cardDataSO.Count);
                float randomVal16_2 = Random.Range(0, cardDataSO.Count);
                float randomVal16_3 = Random.Range(0, cardDataSO.Count);
                float randomVal16_4 = Random.Range(0, cardDataSO.Count);
                float randomVal16_5 = Random.Range(0, cardDataSO.Count);
                float randomVal16_6 = Random.Range(0, cardDataSO.Count);
                randomValCard16.Add((int)randomVal16_1);
                randomValCard16.Add((int)randomVal16_2);
                randomValCard16.Add((int)randomVal16_3);
                randomValCard16.Add((int)randomVal16_4);
                randomValCard16.Add((int)randomVal16_5);
                randomValCard16.Add((int)randomVal16_6);
                break;
        }
    }
    //void CardSpawn()
    //{
    //    for (int i = 0; i < cardsCount; i++)
    //    {
    //        var card = Instantiate(cardPrefab, cardHolder);
    //        card.GetComponent<Card>().cardDataSo = cardDataSO; 
    //        card.GetComponent<Card>().CardSetUp();
    //    }
    //}
}
