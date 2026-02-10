using UnityEngine;

[System.Serializable]
public class Staff
{
    public string staffName;
    public float serviceDuration = 5f; // 完成服務所需秒數
    public int incomePerService = 100; // 每次服務收入

    public Staff(string name, float duration, int income)
    {
        staffName = name;
        serviceDuration = duration;
        incomePerService = income;
    }
}
