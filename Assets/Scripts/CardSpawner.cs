using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardHolder;
    public int cardsCount;
    public float spawnPointRandomPrefabValue;
    public List<CardData> cardDataSO = new List<CardData>();
    public List<Transform> spawnPointsList=new List<Transform>();
    private List<GameObject> selectedCards;
    private List<GameObject> currentCardGameObjects=new List<GameObject>();

    [Header("RANDOM_CARD_VALIDATION")]
    public List<int> randomValCard4=new List<int>(); 
    public List<int> randomValCard6 = new List<int>();
    public List<int> randomValCard8 = new List<int>();  
    public List<int> randomValCard10 = new List<int>();
    public List<int> randomValCard12 = new List<int>();
    public List<int> randomValCard16 = new List<int>();

    [Header("SPAWNPOINTS_PREFAB")]
    public List<GameObject> prefab_Card4;
    public List<GameObject> prefab_Card6;
    public List<GameObject> prefab_Card8;  
    public List<GameObject> prefab_Card10;
    public List<GameObject> prefab_Card12;
    public List<GameObject> prefab_Card16;
    public Transform testSpawnArea;

    // Start is called before the first frame update
    void Start()
    {
        CreateSpawnPoints();
    }

    [ContextMenu("SpawnCards")]
    void CreateSpawnPoints()
    {
        DeleteRandomCards();
        ChooseCardType();
        ChooseSpawnPointPrefab();

        switch (cardsCount)
        {
            case 4:
                CardMatchValidation(randomValCard4);
                CardSpawn(spawnPointsList);
                break;
            case 6:
                CardMatchValidation(randomValCard6);
                CardSpawn(spawnPointsList);
                break;
            case 8:
                CardMatchValidation(randomValCard8);
                CardSpawn(spawnPointsList);
                break;
            case 10:
                CardMatchValidation(randomValCard10);
                CardSpawn(spawnPointsList);
                break;
            case 12:
                CardMatchValidation(randomValCard12);
                CardSpawn(spawnPointsList);
                break;
            case 16:
                CardMatchValidation(randomValCard16);
                CardSpawn(spawnPointsList);
                break;
        }
    }

    void DeleteCardSpawnPointPrefab()
    {
        if (cardHolder.childCount > 0)
        {
            Destroy(cardHolder.GetChild(0).gameObject);
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

    void ClearSpawnPointsList()
    {
        if (spawnPointsList.Count > 0)
        {
            spawnPointsList.Clear();
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
                if (randomVal4_2 == randomVal4_1)
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

    void ChooseSpawnPointPrefab()
    {
        switch (cardsCount)
        {
            case 4:
                if (spawnPointRandomPrefabValue != -1)
                {
                    ClearSpawnPointsList();
                    DeleteCardSpawnPointPrefab();
                }
                else
                {
                    spawnPointRandomPrefabValue = Random.Range(0, prefab_Card4.Count);
                }
                SpawnPointPrefabSpawn(prefab_Card4);
                break;
            case 6:
                if (spawnPointRandomPrefabValue != -1)
                {
                    ClearSpawnPointsList();
                    DeleteCardSpawnPointPrefab();
                }
                else
                {
                    spawnPointRandomPrefabValue = Random.Range(0, prefab_Card6.Count);
                }
                SpawnPointPrefabSpawn(prefab_Card6);
                break;
            case 8:
                if (spawnPointRandomPrefabValue != -1)
                {
                    ClearSpawnPointsList();
                    DeleteCardSpawnPointPrefab();
                }
                else
                {
                    spawnPointRandomPrefabValue = Random.Range(0, prefab_Card8.Count);
                }
                SpawnPointPrefabSpawn(prefab_Card8);
                break;
            case 10:
                if (spawnPointRandomPrefabValue != -1)
                {
                    ClearSpawnPointsList();
                    DeleteCardSpawnPointPrefab();
                }
                else
                {
                    spawnPointRandomPrefabValue = Random.Range(0, prefab_Card10.Count);
                }
                SpawnPointPrefabSpawn(prefab_Card10);
                break;
            case 12:
                if (spawnPointRandomPrefabValue != -1)
                {
                    ClearSpawnPointsList();
                    DeleteCardSpawnPointPrefab();
                }
                else
                {
                    spawnPointRandomPrefabValue = Random.Range(0, prefab_Card12.Count);
                }
                SpawnPointPrefabSpawn(prefab_Card12);
                break;
            case 16:
                if (spawnPointRandomPrefabValue != -1)
                {
                    ClearSpawnPointsList();
                    DeleteCardSpawnPointPrefab();
                }
                else
                {
                    spawnPointRandomPrefabValue = Random.Range(0, prefab_Card16.Count);
                }
                SpawnPointPrefabSpawn(prefab_Card16);
                break;
        }

    }

    void SpawnPointPrefabSpawn(List<GameObject> prefabList)
    {
        var spawnPointsPrefab = Instantiate(prefabList[(int)spawnPointRandomPrefabValue], cardHolder);
        for (int i = 0; i < cardsCount; i++)
        {
            spawnPointsList.Add(spawnPointsPrefab.transform.GetChild(i));
        }
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
            else if (i >= 12 && i < 16)
            {
                CardCreation(randCardNumberList[5]);
            }
        }
    }

    void CardCreation( int randomValueList)
    {
        var card = Instantiate(cardPrefab, testSpawnArea);
        Card cardScript = card.GetComponent<Card>();
        cardScript.cardDataSo = cardDataSO[randomValueList];
        cardScript.CardSetUp();
        currentCardGameObjects.Add(card);
    }  
    
    void CardSpawn(List<Transform> spawnPosList)
    {   
        for (int i = 0; i < spawnPosList.Count; i++)
        {
            float randomId = Random.Range(0,currentCardGameObjects.Count);
            //Debug.Log("RID: " + randomId + "CC: "+ currentCardGameObjects.Count);
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

   
}
