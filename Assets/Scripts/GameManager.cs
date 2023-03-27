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
    public int WheatToWarriors = 2;
    public int NextRaidCount = 0;
    public int RaidCountIncrease = 1;
    public int WavesDelay = 3;
    public int PeasantsToWin = 10;

    public ImageScript HarvestTimer;
    public ImageScript EatTimer;
    public ImageScript RaidTimer;

    public TextMeshProUGUI resourcesText;
    public TextMeshProUGUI nextWaveCountText;
    public TextMeshProUGUI buyPeasantBtnText;
    public TextMeshProUGUI buyWarriorBtnText;
    public TextMeshProUGUI dieScreenSummaryText;
    public TextMeshProUGUI winScreenSummaryText;
    public GameObject GameOverScreen;
    public GameObject WinScreen;
    public GameObject PauseScreen;
    public Button MuteButton;


    private int _wavesSurvived = 0;
    private int _warriorsBought = 0;
    private int _peasantsBought = 0;
    private int _wheatManufactured = 0;

    private SoundsPlayer _soundsPlayer;

    void Start()
    {
        _soundsPlayer = GetComponent<SoundsPlayer>();

        buyPeasantBtnText.text += $"({PeasantCost})";
        buyWarriorBtnText.text += $"({WarriorCost})";

        UpdateTexts();
    }

    void Update()
    {
        if (HarvestTimer.Tick)
        {
            var manufactured = WheatPerPeasant * PeasantCount;
            WheatCount += manufactured;
            _wheatManufactured += manufactured;

            UpdateTexts();
        }

        if (EatTimer.Tick)
        {
            var toEat = WheatToWarriors * WarriorCount;
            WheatCount -= toEat;

            if (toEat > 0) {
                _soundsPlayer.Eat();
            }

            if (WheatCount < 0)
            {
                WheatCount = 0;
            }
            UpdateTexts();
        }

        if (RaidTimer.Tick)
        {

            _wavesSurvived++;

            if (WavesDelay > 0)
            {
                WavesDelay--;
            }
            else
            {
                WarriorCount -= NextRaidCount;
                NextRaidCount += RaidCountIncrease;
                _soundsPlayer.Fight();
            }

            UpdateTexts();

            if (WarriorCount < 0)
            {
                GameOver();
            }
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
            
            _soundsPlayer.Spawn();
            _peasantsBought++;

            UpdateTexts();
            if (PeasantCount >= PeasantsToWin) {
                Win();
            }
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
            _soundsPlayer.Spawn();
            _warriorsBought++;

            WarriorButton.interactable = true;
            UpdateTexts();
        });
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        PeasantButton.interactable = WheatCount >= PeasantCost;
        WarriorButton.interactable = WheatCount >= WarriorCost;

        resourcesText.text = $"Крестьяне: {PeasantCount}\n\nВоины: {WarriorCount}\n\nПшеница: {WheatCount}";
        nextWaveCountText.text = $"Прийдет {NextRaidCount} бандитов";
    }

    private void GameOver()
    {
        dieScreenSummaryText.text = $"Вы проиграли: \n\n" +
            $"Атак пережито: {_wavesSurvived}\n" +
            $"Собрано зерна: {_wheatManufactured}\n" +
            $"Воинов нанято: {_warriorsBought}\n" +
            $"Крестьян нанято: {_peasantsBought}\n";

        Time.timeScale = 0;
        GameOverScreen.SetActive(true);
    }

    private void Win() {
        Time.timeScale = 0;

        winScreenSummaryText.text = $"Вы выиграли: \n\n" +
            $"Крестьян нанято: {_peasantsBought}";    
        WinScreen.SetActive(true);
    }

    public void EnableSound() {
        var audio = GameObject.Find("AudioSources").GetComponent<AudioSource>();
        var soundImg = GameObject.Find("SoundButton").GetComponent<Image>();

        if (audio.mute) {
            audio.mute = false;
            soundImg.color = Color.white;
        } else {
            audio.mute = true;
            soundImg.color = Color.gray;
        }
    }

    public void Pause() {
        bool isPaused = Time.timeScale == 0;
        PauseScreen.SetActive(!isPaused);
        Time.timeScale = isPaused ? 1 : 0;
    }
}
