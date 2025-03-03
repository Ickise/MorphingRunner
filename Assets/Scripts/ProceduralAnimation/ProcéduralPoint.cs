using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcéduralPoint : MonoBehaviour
{
    // Problème de nomenclature et de lissibilité.
    // Je pense même que cela était possible de rendre ce script, PointMove et animatonQueu plus générique.
    // En encapsulant les comportements communs dans une classe mère et en dérivant les spécificités dans des classes filles.

    [SerializeField] private Vector3 _shift = Vector3.up;
    [SerializeField] private Vector3 _sensorDirection = Vector3.down;
    [SerializeField] private float _maxDetectionDistance = 2;

    [SerializeField, Range(0.1f, 1f)] private float _radius = 0.3f;

    [SerializeField] private LayerMask _layerMaskDetection;

    private float _floorDistance = 0;
    private bool _floorDetected = false;

    private void FixedUpdate()
    {
        // Nous effectuons cette détection à chaque FixedUpdate, ce qui est bien pour la physique, mais cela pourrait être appelé uniquement si nécessaire.
        
        RaycastHit hit;
        _floorDetected = false;

        // SphereCast peut être gourmand en performances s’il est utilisé sur plusieurs objets à chaque frame.
        // Une alternative serait d’optimiser la fréquence des appels ou d’utiliser un simple Raycast si un SphereCast n’est pas nécessaire.
        
        if (Physics.SphereCast(transform.position + _shift, _radius, _sensorDirection, out hit, _maxDetectionDistance, _layerMaskDetection))
        {
            // Calcul de la distance du sol en tenant compte du décalage initial (_shift).
            _floorDistance = hit.distance - Vector3.Project(_shift, _sensorDirection).magnitude;
            _floorDetected = true;

            // Debug.DrawLine est utile pour le Debug, mais inutile en production.
            // Il pourrait être encapsulé dans une condition #if UNITY_EDITOR pour éviter les calculs inutiles en build final.
            Debug.DrawLine(transform.position + _shift, hit.point, Color.green, Time.fixedDeltaTime);
            return;
        }

        // Si aucun sol détecté, nous dessinons un rayon rouge pour visualiser l'absence de détection.
        Debug.DrawLine(transform.position + _shift, transform.position + _sensorDirection.normalized * _maxDetectionDistance, Color.red, Time.fixedDeltaTime);
    }

    public float GetDistance()
    {
        // Fonction publique pour récupérer la distance du sol.
        // Cette fonction pourrait retourner également un bool pour éviter d'appeler une autre variable externe.
        return _floorDistance;
    }
}
