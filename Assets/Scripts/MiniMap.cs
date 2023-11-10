using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private Transform player;
    float storedShadowDistance;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
    }

    void OnPreRender()
    {
        storedShadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }

    void OnPostRender()
    {
        QualitySettings.shadowDistance = storedShadowDistance;
    }

}
