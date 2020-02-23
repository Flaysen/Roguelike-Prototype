using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDStats : MonoBehaviour
{
    private PlayerStats playerStats;

    [SerializeField] private KeyCode showStatsKey;

    [SerializeField] private TextMeshProUGUI healthValue;

    [SerializeField] private TextMeshProUGUI energyValue;

    [SerializeField] private TextMeshProUGUI armourValue;

    [SerializeField] private TextMeshProUGUI speedValue;

    [SerializeField] private TextMeshProUGUI attackRatioValue;

    [SerializeField] private TextMeshProUGUI attackSpeedValue;

    [SerializeField] private TextMeshProUGUI attackRangeValue;

    [SerializeField] private TextMeshProUGUI luckValue;


    private void Update()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        ShowStatsHudOnKeyHold();


        healthValue.text = playerStats.MaxHealth.Value.ToString();

        energyValue.text = playerStats.MaxEnergy.Value.ToString();    

        armourValue.text = playerStats.Armour.Value.ToString();

        speedValue.text = playerStats.Speed.Value.ToString();

        attackRatioValue.text = playerStats.AttackRatio.Value.ToString();

        attackSpeedValue.text = playerStats.AttackSpeed.Value.ToString();

        attackSpeedValue.text = playerStats.AttackSpeed.Value.ToString();

        luckValue.text = playerStats.Luck.Value.ToString();

    }

    private void ShowStatsHudOnKeyHold()
    {
        if(Input.GetKey(showStatsKey))
        {
            Image[] images = transform.GetComponentsInChildren<Image>();
            foreach (Image i in images)
            {
                i.enabled = true;
                i.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            }
        }
        else
        {
            Image[] images = transform.GetComponentsInChildren<Image>();
            foreach (Image i in images)
            {
                i.enabled = false;
                i.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
    }
}
