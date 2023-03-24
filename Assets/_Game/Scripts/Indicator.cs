using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    public GameObject nextStop;

    private RectTransform indicatorRectTransform;
    private Camera mainCamera;

    private void Start()
    {
        // get reference to the UI image
        indicatorRectTransform = GetComponent<RectTransform>();
        // get reference to the main camera
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (nextStop == null)
            return;

        Vector3 targetPosition = nextStop.transform.position;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetPosition);

        // Check if the target is behind the camera
        if (screenPosition.z < 0)
            screenPosition *= -1;

        // Calculate the position of the indicator image
        Vector2 indicatorPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(indicatorRectTransform.parent as RectTransform, screenPosition, null, out indicatorPosition);

        // Set the position and rotation of the indicator image
        indicatorRectTransform.localPosition = indicatorPosition;
        indicatorRectTransform.rotation = Quaternion.LookRotation(Vector3.forward, targetPosition - transform.position);
    }

    public List<Image> indicatorList;
    private bool _isHadObject;
    public bool isCreateNew;
    public Transform containIndicator;

    public Image SpawnIndicator()
    {
        for (int i = 0; i < indicatorList.Count; i++)
        {
            if (!indicatorList[i].gameObject.activeSelf)
            {
                indicatorList[i].gameObject.SetActive(true);
                _isHadObject = true;
                return indicatorList[i];
            }
            else
            {
                _isHadObject = false;
            }
        }
        if (isCreateNew)
        {
            if (!_isHadObject)
            {
                Image more = Instantiate(indicatorList[0], containIndicator);
                more.gameObject.SetActive(true);
                indicatorList.Add(more);
                return more;
            }
        }
        return null;

    }
}
