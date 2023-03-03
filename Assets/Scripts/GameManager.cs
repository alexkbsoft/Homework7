using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ImageScript PeasantBuildTimer;
    public Button PeasantButton;
    public int PeasantCount = 0;
    public float PeasantCreateTime = 3;
    public int PeasantCost = 2;
    public int WarriorCost = 4;

    public ImageScript WarriorBuildTimer;
    public Button WarriorButton;
    public int WarriorCount = 0;
    public float WarriorCreateTime = 2.0f;

    public int WheatCount = 0;
    public int WheatPerPeasant = 1;

    public ImageScript HarvestTimer;
    public ImageScript EatTimer;

    public TextMeshProUGUI resourcesText;
    public int WheatToWarriors = 2;
    void Start()
    {
        UpdateTexts();
    }

    // Update is called once per frame
    void Update()
    {
        if (HarvestTimer.Tick)
        {
            WheatCount += WheatPerPeasant * PeasantCount;
            UpdateTexts();
        }

        if (EatTimer.Tick)
        {
            WheatCount -= WheatToWarriors * WarriorCount;
            if (WheatCount < 0)
            {
                WheatCount = 0;
            }
            UpdateTexts();
        }
    }

    public void OnBuildPeasant()
    {
        if (WheatCount < PeasantCost) return;

        PeasantButton.interactable = false;
        WheatCount -= PeasantCost;
        PeasantBuildTimer.Play(PeasantCreateTime, () =>
        {
            PeasantCount++;
            PeasantButton.interactable = true;
            UpdateTexts();
        });
        UpdateTexts();
    }

    public void OnBuildWarrior()
    {
        if (WheatCount < WarriorCost) return;

        WarriorButton.interactable = false;
        WheatCount -= WarriorCost;
        WarriorBuildTimer.Play(WarriorCreateTime, () =>
        {
            WarriorCount++;
            WarriorButton.interactable = true;
            UpdateTexts();
        });
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        resourcesText.text = $"Крестьяне: {PeasantCount}\n\nВоины: {WarriorCount}\n\nПшеница: {WheatCount}";
    }
}
