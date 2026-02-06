using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [Header("建築資訊")]
    public string buildingName = "建築物";
    public int cost = 100;
    
    // 佔地尺寸 (例如 1x1, 2x2)
    public Vector2Int size = new Vector2Int(1, 1);

    public virtual void OnPlaced()
    {
        Debug.Log($"{buildingName} 建造完成！");
    }
}
