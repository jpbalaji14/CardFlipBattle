using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardType
{
    A,B,C,D,E,F
}

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class CardData : ScriptableObject
{
    public string cardName;
    public GameObject cardItemGameObject;
    public CardType cardType;
    public int cardLevel;
    public float cardPower;
    public Material cardMaterial;

}
