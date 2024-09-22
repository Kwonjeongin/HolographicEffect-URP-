using UnityEngine;

public class MaterialControl : MonoBehaviour
{
    public Camera mainCamera; // 카메라
    public GameObject targetObject; // 대상 오브젝트
    public Material defaultMaterial; // 기본 머티리얼
    public Material highlightedMaterial; // 바뀌는 머티리얼

    public float rayDistance = 10f; // 레이의 거리
    public float sphereRadius = 0.5f; // 구의 반지름 (폭 조절)

    private void Start()
    {
        // 기본 머티리얼 설정
        if (targetObject != null)
        {
            Renderer renderer = targetObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = defaultMaterial;
            }
        }
    }

    private void Update()
    {
        // 카메라에서 나오는 레이
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // 구체의 위치
        Vector3 spherePosition = ray.origin + ray.direction * rayDistance;

        // 구체로 충돌 감지
        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, sphereRadius);

        bool hitDetected = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == targetObject)
            {
                hitDetected = true;
                break;
            }
        }

        // 머티리얼 변경
        Renderer targetRenderer = targetObject.GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            targetRenderer.material = hitDetected ? highlightedMaterial : defaultMaterial;
        }

        // 구체의 위치를 시각적으로 표시 (디버그용)
        Debug.DrawLine(ray.origin, spherePosition, Color.red);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.blue);
    }
}
