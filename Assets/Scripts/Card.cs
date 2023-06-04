using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardData cardDataSo;
    public MeshRenderer cardMat;
    public float cardPower;
    public int cardLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardSetUp()
    {
        cardPower = cardDataSo.cardPower;
        cardLevel = cardDataSo.cardLevel;
        //cardMat.material = cardDataSo.cardMaterial;
    }
}
