using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName;
    public CardData cardDataSo;
    public MeshRenderer cardMat;
    public float cardPower;
    public int cardLevel;
    public GameObject CardHighlight;

    public void CardSetUp()
    {
        cardName = cardDataSo.cardName;
        cardPower = cardDataSo.cardPower;
        cardLevel = cardDataSo.cardLevel;
        cardMat.material = cardDataSo.cardMaterial;
    }

    public void CardSelect()
    {
        CardHighlight.SetActive(true);
        CardCombo combo = new CardCombo();
        combo.cardName = this.gameObject.name;
        combo.cardScript = this;
        combo.cardGameObject = this.gameObject;
        combo.cardTransform = this.gameObject.transform.parent;
        combo.cardScore = cardPower;
        GameManager.Instance.cardCombo.Add(combo);
        Debug.Log(combo.cardName + "Selected");
        if (GameManager.Instance.isOpponentTurn)
        {
            Invoke(nameof(CardValidation), 1.5f);
        }
        else
        {
            GameManager.Instance.CardMatchValidation();
        }
    }
    void CardValidation()
    {
        GameManager.Instance.CardMatchValidation();
    }
    public void CardMatched()
    {
        Debug.Log("Card Matched");
    }
    public void CardNotMatched()
    {
        CardHighlight.SetActive(false);
        Debug.Log("Card Not matched");
    }
}
