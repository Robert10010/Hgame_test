using UnityEngine;
using System.Collections.Generic;

public class Brothel : Building
{
    [Header("娼館設定")]
    public int maxStaff = 3;
    private List<Staff> activeStaff = new List<Staff>();

    // 用於追蹤每個員工的服務進度
    private Dictionary<Staff, float> serviceTimers = new Dictionary<Staff, float>();

    public override void OnPlaced()
    {
        base.OnPlaced();
        // 測試用：自動添加一個員工
        AssignStaff(new Staff("小美", 5f, 100));
    }

    public void AssignStaff(Staff staff)
    {
        if (activeStaff.Count < maxStaff)
        {
            activeStaff.Add(staff);
            serviceTimers[staff] = 0f;
            Debug.Log($"指派員工 {staff.staffName} 到 {buildingName}");
        }
        else
        {
            Debug.Log("員工位置已滿！");
        }
    }

    private void Update()
    {
        // 處理每個員工的服務進度
        for (int i = 0; i < activeStaff.Count; i++)
        {
            Staff staff = activeStaff[i];
            serviceTimers[staff] += Time.deltaTime;

            if (serviceTimers[staff] >= staff.serviceDuration)
            {
                CompleteService(staff);
            }
        }
    }

    private void CompleteService(Staff staff)
    {
        // 重置計時器
        serviceTimers[staff] = 0f;
        
        // 獲得收益
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddMoney(staff.incomePerService);
            Debug.Log($"員工 {staff.staffName} 完成服務，獲得 {staff.incomePerService} 元");
        }
    }
}
