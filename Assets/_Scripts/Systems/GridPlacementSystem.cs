using UnityEngine;

public class GridPlacementSystem : MonoBehaviour
{
    [Header("建造設定")]
    public Building selectedBuildingPrefab;
    public LayerMask groundLayer;
    public float gridSize = 1f;

    private GameObject previewObject;

    private void Update()
    {
        if (selectedBuildingPrefab == null) return;
        
        // 檢查滑鼠是否存在
        if (UnityEngine.InputSystem.Mouse.current == null) return;

        // 1. 射線檢測滑鼠位置
        Vector2 mousePos = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            Vector3 snapPos = SnapToGrid(hit.point);

            // 2. 更新預覽位置
            if (previewObject == null)
            {
                previewObject = Instantiate(selectedBuildingPrefab.gameObject);
                // 移除腳本避免預覽時運行邏輯
                Destroy(previewObject.GetComponent<Building>()); 
            }
            previewObject.transform.position = snapPos;

            // 3. 點擊建造
            if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
            {
                TryBuild(snapPos);
            }
        }
        else
        {
            if (previewObject != null)
            {
                Destroy(previewObject);
                previewObject = null;
            }
        }
    }

    private Vector3 SnapToGrid(Vector3 original)
    {
        float x = Mathf.Round(original.x / gridSize) * gridSize;
        float z = Mathf.Round(original.z / gridSize) * gridSize;
        return new Vector3(x, original.y, z);
    }

    private void TryBuild(Vector3 position)
    {
        if (GameManager.Instance.Money >= selectedBuildingPrefab.cost)
        {
            GameManager.Instance.SpendMoney(selectedBuildingPrefab.cost);
            Building newBuilding = Instantiate(selectedBuildingPrefab, position, Quaternion.identity);
            newBuilding.OnPlaced();
            Debug.Log($"在 {position} 建造了 {selectedBuildingPrefab.buildingName}");
        }
        else
        {
            Debug.Log("無法建造：金錢不足");
        }
    }
}
