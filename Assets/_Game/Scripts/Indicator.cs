using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    public Transform[] targets;  // Mảng các Transform của các targets
    public Image pointerImage;  // Tham chiếu đến UI Image object được sử dụng cho pointer
    public float maxDistance = 10f;  // Khoảng cách tối đa giữa indicator và target

    private Transform closestTarget;  // Transform của target gần nhất
    private float closestDistance = Mathf.Infinity;  // Khoảng cách đến target gần nhất

    void Update()
    {
        // Tìm target gần nhất
        foreach (Charecter target in GameManager.GetInstance().listTarget)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget < closestDistance)
            {
                closestTarget = target.transform;
                closestDistance = distanceToTarget;
            }
        }

        // Nếu không có target, không hiển thị pointer
        if (closestTarget == null)
        {
            pointerImage.enabled = false;
            return;
        }

        // Nếu khoảng cách đến target gần nhất lớn hơn maxDistance, không hiển thị pointer
        if (closestDistance > maxDistance)
        {
            pointerImage.enabled = false;
            return;
        }

        // Hiển thị pointer và tính toán hướng và góc xoay
        pointerImage.enabled = true;
        Vector3 direction = closestTarget.position - transform.position;
        direction.z = 0f;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        pointerImage.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
