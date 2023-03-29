using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{

    public Transform target; // Đối tượng cần chỉ dẫn
    public Image pointerUI; // UI Image để hiển thị mũi tên

    private RectTransform pointerRectTransform;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main; // Lấy Camera chính trong Scene
        pointerRectTransform = pointerUI.GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 targetPos = mainCamera.WorldToViewportPoint(target.position); // Chuyển đổi vị trí đối tượng cần chỉ dẫn sang ViewportPoint
        bool isOffScreen = (targetPos.x < 0 || targetPos.x > 1 || targetPos.y < 0 || targetPos.y > 1);
        if (isOffScreen)
        {
            Vector3 cappedTargetScreenPosition = targetPos;
            cappedTargetScreenPosition.x = Mathf.Clamp01(cappedTargetScreenPosition.x);
            cappedTargetScreenPosition.y = Mathf.Clamp01(cappedTargetScreenPosition.y);

            Vector3 pointerWorldPosition = mainCamera.ViewportToWorldPoint(cappedTargetScreenPosition); // Chuyển đổi lại vị trí Mũi tên vào thế giới 3D

            pointerRectTransform.position = pointerWorldPosition; // Cập nhật vị trí mới cho Mũi tên
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f); // Xóa thông số Z của localPosition để nó hiển thị đúng trên dòng chữ
            pointerRectTransform.rotation = Quaternion.LookRotation(transform.position - pointerWorldPosition, Vector3.forward); // Xử lý xoay Mũi tên
        }
    }
}