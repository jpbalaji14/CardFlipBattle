using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public string cardName;
    public CardData cardDataSo;
    public MeshRenderer cardMat;
    public float cardPower;
    public int cardLevel;
    public GameObject CardHighlight;
    public Transform cardItemSpawnPoint;
    public GameObject cardGameObject;

    public void CardSetUp()
    {
        cardName = cardDataSo.cardName;
        cardPower = cardDataSo.cardPower;
        cardLevel = cardDataSo.cardLevel;
        cardMat.material = cardDataSo.cardMaterial;
        cardGameObject = Instantiate(cardDataSo.cardItemGameObject, cardItemSpawnPoint);
        cardGameObject.transform.localPosition = new Vector3(0, -2, 0);
    }

    public void CardSelect()
    {
        //cardGameObject.transform.localPosition = new Vector3(0, -2, 0);
        this.transform.DORotate(new Vector3(0, 0, 180), 0.5f);
        CardHighlight.SetActive(true);
        CardCombo combo = new CardCombo();
        combo.cardName = this.gameObject.name;
        combo.cardScript = this;
        combo.cardGameObject = this.gameObject;
        combo.cardTransform = this.gameObject.transform.parent;
        combo.cardScore = cardPower;
        GameManager.Instance.cardCombo.Add(combo);
        Debug.Log(combo.cardName + "Selected" + GameManager.Instance.isOpponentTurn);
        if (GameManager.Instance.isOpponentTurn)
        {
            Debug.Log("Enable opp interact 2");
            StartCoroutine(CardValidation(1));
        }
        else
        {
            if (GameManager.Instance.cardCombo.Count == 2)
            {
                StartCoroutine(CardValidation(0.5f));
            }
            else
            {
                GameManager.Instance.CardMatchValidation();
            }
        }
    }
    IEnumerator CardValidation(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Enable opp interact 3");
        GameManager.Instance.CardMatchValidation();
    }
    public IEnumerator CardMatched()
    {
        Debug.Log("Card Matched");

        if (GameManager.Instance.isPlayerTurn)
        {
            cardItemSpawnPoint.GetChild(0).transform.DOMove(GameManager.Instance.playerAttackPoint.position, 0.5f)
                .SetEase(Ease.Linear);

            yield return new WaitForSeconds(0.5f);

            cardItemSpawnPoint.GetChild(0).transform.DOMove(GameManager.Instance.opponentPosition.position, 1)
                .SetEase(Ease.Linear);
        }
        else
        {
            cardItemSpawnPoint.GetChild(0).transform.DOMove(GameManager.Instance.opponentAttackPoint.position, 0.5f)
                .SetEase(Ease.Linear);

            yield return new WaitForSeconds(0.5f);

            cardItemSpawnPoint.GetChild(0).transform.DOMove(GameManager.Instance.playerPosition.position, 1)
                .SetEase(Ease.Linear);
        }
        yield return new WaitForSeconds(1f);
        GameManager.Instance.OnCardMatched();
      
        this.gameObject.SetActive(false);


    }
    public void CardNotMatched()
    {
        this.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        CardHighlight.SetActive(false);
        Debug.Log("Card Not matched");
    }
}
