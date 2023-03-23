using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCanvas : MonoBehaviour
{
    [SerializeField] Slider bossHealthSlider;
    [SerializeField] BossScript boss;
    void Start()
    {
        bossHealthSlider.maxValue = boss.GetBossHealth();
    }

    // Update is called once per frame
    void Update()
    {
        bossHealthSlider.value = boss.GetBossHealth();
    }
}
