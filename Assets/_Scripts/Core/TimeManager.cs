using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("時間設定")]
    [Tooltip("一整天的持續時間（秒）")]
    public float dayDuration = 60f; 
    
    [Range(0, 1)]
    public float timeOfDay = 0.25f; // 0.25 = 早上, 0.5 = 中午, 0.75 = 傍晚, 0.0 = 午夜

    public UnityEvent OnDayStart;
    public UnityEvent OnNightStart;

    private bool isNight = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        timeOfDay += Time.deltaTime / dayDuration;

        if (timeOfDay >= 1f)
        {
            timeOfDay = 0f;
        }

        CheckDayNightEvents();
    }

    private void CheckDayNightEvents()
    {
        bool currentIsNight = (timeOfDay < 0.25f || timeOfDay > 0.75f);

        if (currentIsNight != isNight)
        {
            isNight = currentIsNight;
            if (isNight)
            {
                OnNightStart?.Invoke();
                Debug.Log("入夜了");
            }
            else
            {
                OnDayStart?.Invoke();
                Debug.Log("天亮了");
            }
        }
    }
}
