using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBar : MonoBehaviour, IHealth {

    public HealthHeart heart;
    private List<HealthHeart> HeartsList =new List<HealthHeart>();

    int AmountHearts = 0;

    public void DecreasingHealth(int decrementHealth)
    {
        decrementHealth = Mathf.Clamp(decrementHealth, 0, 10);
        DestroyHearts(decrementHealth);
        SetAmountHearts(decrementHealth);
        UpdatePositionAfterChangeHeartsAmount();
    }

    void UpdatePositionAfterChangeHeartsAmount()
    {
        for (int i = 0; i < AmountHearts; i++)
        {
            Vector3 HeartPosition = new Vector3((i - AmountHearts / 2) *0.5f, -0.5f, 0f);
            HeartsList[i].transform.localPosition = HeartPosition;
        }      
    }

    void SetAmountHearts(int Amount)
    {
        AmountHearts -= Amount;
    }

    void DestroyHearts(int HeartsToDestroy)
    {
        for (int i = 1; i <= HeartsToDestroy; i++)
        {
            HealthHeart SelectetdHeart = HeartsList[AmountHearts - i];
            HeartsList.Remove(SelectetdHeart);
            Destroy(SelectetdHeart.gameObject);
        }
    }

    public void MaxHealth(int health)
    {
        AmountHearts = health;
        SpawnHeartOnStart();
    }

    void SpawnHeartOnStart()
    {
        for (int i = 0; i < AmountHearts; i++)
        {
            Vector3 HeartPosition = new Vector3((i-AmountHearts/2) * 0.35f,-0.5f,0f);
      
            HealthHeart freshHeart = Instantiate(heart, gameObject.transform.position+ HeartPosition,Quaternion.identity);
            freshHeart.transform.SetParent(gameObject.transform);
            HeartsList.Add(freshHeart);
        }
    }
}

