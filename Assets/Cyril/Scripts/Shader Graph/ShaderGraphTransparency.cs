using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderGraphTransparency : MonoBehaviour
{
    [SerializeField] 
    private Transform _player;
    [SerializeField]
    private LayerMask wallMask;
    private Camera mainCamera;
    private void Awake() {
        mainCamera = GetComponent<Camera>();
    }
    void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(_player.position);
        cutoutPos.y /= (Screen.width / Screen.height);
        Vector3 offset = _player.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position,offset,offset.magnitude,wallMask);
        Debug.DrawRay(transform.position,offset,Color.blue,10f);
        for (int i = 0; i < hitObjects.Length; i++)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;
            for (int m = 0; m < materials.Length; m++)
            {
                materials[m].SetVector("_cutoutPosition", cutoutPos);
                materials[m].SetFloat("_cutoutSize", 0.15f);
                materials[m].SetFloat("_falloffSize", 0.01f);
            }
        }
    }
}
