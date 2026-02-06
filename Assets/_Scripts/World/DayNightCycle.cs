using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private Light directionalLight;

    [Header("Settings")]
    public Vector3 noonEulerRotation = new Vector3(90, 0, 0);

    private void Update()
    {
        if (timeManager == null) return;

        // Map time (0-1) to rotation (0-360)
        // 0.25 (Morning) -> 0 degrees X (Horizon) - wait, standard unity light:
        // 90 is straight down (Noon).
        // Let's assume:
        // 0.0 (Midnight) -> -90
        // 0.25 (6 AM) -> 0
        // 0.5 (Noon) -> 90
        // 0.75 (6 PM) -> 180
        
        float angle = (timeManager.timeOfDay * 360f) - 90f;
        
        if (directionalLight != null)
        {
            directionalLight.transform.rotation = Quaternion.Euler(angle, -30f, 0f);
        }
    }

    private void OnValidate()
    {
        if (timeManager == null)
            timeManager = FindObjectOfType<TimeManager>();
        
        if (directionalLight == null)
            directionalLight = GetComponent<Light>();
    }
}
