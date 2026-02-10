using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("經濟系統")]
    [SerializeField] private int money = 1000;
    public int Money => money;

    public UnityEvent<int> OnMoneyChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        OnMoneyChanged?.Invoke(money);
        Debug.Log($"增加金錢: {amount}。總計: {money}");
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            OnMoneyChanged?.Invoke(money);
            Debug.Log($"花費金錢: {amount}。總計: {money}");
            return true;
        }
        
        Debug.Log("金錢不足！");
        return false;
    }
}
