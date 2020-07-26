using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public Image HpGauge;
    public Image TimeGauge;
    public Image BossHPGauge;

    public Text HpText;
    public Text TimeText;

    public GameObject ClearPanel;
    public GameObject FailPanel;

    private int hpValue = 0;
    private int timeValue = 0;
    private int bossHpValue = 0;

    private void Start()
    {
    }

    private void Update()
    {
        HpGauge.fillAmount = Mathf.Lerp(HpGauge.fillAmount, (hpValue / 10.0f), 7.5f * Time.deltaTime);
        TimeGauge.fillAmount = Mathf.Lerp(TimeGauge.fillAmount, (timeValue / 100.0f), 7.5f * Time.deltaTime);
        BossHPGauge.fillAmount = Mathf.Lerp(BossHPGauge.fillAmount, (bossHpValue / 100.0f), 7.5f * Time.deltaTime);
    }

    public void SetHpGauge(int hp)
    {
        hpValue = hp;
        HpText.text = hp.ToString();
    }

    public void SetTimeGauge(int time)
    {
        timeValue = time;
        TimeText.text = time.ToString();
    }

    public void SetBossHpGauge(int hp)
    {
        bossHpValue = hp;
    }

    public void SeeClearScreen()
    {
        ClearPanel.SetActive(true);
    }

    public void SeeFailScreen()
    {
        FailPanel.SetActive(true);
    }
}