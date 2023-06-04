using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "SpawnSet", menuName = "ScriptableObjects/SpawnSetObject", order = 1)]
[System.Serializable]
public class SpawnPosition
{
    public List<Transform> spawnPositionList;
    public Vector3 spawnHolderPosition;
}
public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardHolder;
    public int cardsCount;
    public CardData cardDataSO;
    //public List<Transform> currentSpawnPoints;   
    public List<Transform> spawnPointsList;
    public List<GameObject> selectedCards;
    public float spawnRandomValue;

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
       // CardSpawn();
    }


    [ContextMenu("Test")]
    void CreateSpawnPoints()
    {
       // if(spawnRandomValue == -1)
        {
            ClearSpawnPoints();
            float randValue = Random.RandomRange(0, 2);
            spawnRandomValue = randValue;
            Debug.Log("Spawnpoint Random Value : " + randValue);
        }

        switch (cardsCount)
        {
            case 4:
                cardHolder.position = cards4.spawnHolderPosition;
                for (int i = 0; i < cards4.spawnPositionList.Count; i++)
                {
                    cards4.spawnPositionList[i].gameObject.SetActive(true);
                }
                break;
            case 6:
                if (spawnRandomValue == 0)
                {
                    cardHolder.position = cards6_0.spawnHolderPosition;
                    for (int i = 0; i < cards6_0.spawnPositionList.Count; i++)
                    {
                        cards6_0.spawnPositionList[i].gameObject.SetActive(true);
                    }
                }
                else
                {
                    cardHolder.position = cards6_1.spawnHolderPosition;
                    for (int i = 0; i < cards6_1.spawnPositionList.Count; i++)
                    {
                        cards6_1.spawnPositionList[i].gameObject.SetActive(true);
                    }
                }
                break;
            case 8:
                if (spawnRandomValue == 0)
                {
                    cardHolder.position = cards8_0.spawnHolderPosition;
                    for (int i = 0; i < cards8_0.spawnPositionList.Count; i++)
                    {
                        cards8_0.spawnPositionList[i].gameObject.SetActive(true);
                    }
                }
                else
                {
                    cardHolder.position = cards8_1.spawnHolderPosition;
                    for (int i = 0; i < cards8_1.spawnPositionList.Count; i++)
                    {
                        cards8_1.spawnPositionList[i].gameObject.SetActive(true);
                    }

                }
                break;
            case 10:
                if (spawnRandomValue == 0)
                {
                    cardHolder.position = cards10_0.spawnHolderPosition;
                    for (int i = 0; i < cards10_0.spawnPositionList.Count; i++)
                    {
                        cards10_0.spawnPositionList[i].gameObject.SetActive(true);
                    }
                }
                else
                {
                    cardHolder.position = cards10_1.spawnHolderPosition;
                    for (int i = 0; i < cards10_1.spawnPositionList.Count; i++)
                    {
                        cards10_1.spawnPositionList[i].gameObject.SetActive(true);
                    }
                }
                break;
            case 12:
                if (spawnRandomValue == 0)
                {
                    cardHolder.position = cards12_0.spawnHolderPosition;
                    for (int i = 0; i < cards12_0.spawnPositionList.Count; i++)
                    {
                        cards12_0.spawnPositionList[i].gameObject.SetActive(true);
                    }
                }
                else
                {
                    cardHolder.position = cards12_1.spawnHolderPosition;
                    for (int i = 0; i < cards12_1.spawnPositionList.Count; i++)
                    {
                        cards12_1.spawnPositionList[i].gameObject.SetActive(true);
                    }
                }
                break;
            case 16:
                cardHolder.position = cards16.spawnHolderPosition;
                for (int i = 0; i < cards16.spawnPositionList.Count; i++)
                {
                    cards16.spawnPositionList[i].gameObject.SetActive(true);
                }
                break;
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


    void CardSpawn()
    {
        for (int i = 0; i < cardsCount; i++)
        {
            var card = Instantiate(cardPrefab, cardHolder);
            card.GetComponent<Card>().cardDataSo = cardDataSO; 
            card.GetComponent<Card>().CardSetUp();
        }
    }
}
