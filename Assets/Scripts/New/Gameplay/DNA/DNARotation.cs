using UnityEngine;

public class DNARotation : MonoBehaviour, IUpdate
{
    // Ce script était auparavant DNARotation, il a juste été modifié et amélioré pour permettre une animation de l'ADN. 
    [SerializeField, Header("Settings")] private float speedRotation = 90f;
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;

    private Transform dnaTransform;
    
    private Vector3 positionOffset;
    private Vector3 tempPosition;
    
    private void OnEnable()
    {
        // Nous nous inscrivons à l'UpdateManager pour que UpdateTick() soit appelé chaque frame.
        UpdateManager.RegisterUpdate(this);
    }

    private void OnDisable()
    {
        // Nous nous désinscrivons de l'UpdateManager pour éviter des appels inutiles une fois le script désactivé.
        UpdateManager.UnregisterUpdate(this);
    }

    private void Start()
    {
        dnaTransform = transform; // Nous récupérons le transform de l'objet pour faciliter son accès.
        positionOffset = dnaTransform.position; // Nous enregistrons la position initiale pour y ajouter l'oscillation.
    }

    public void UpdateTick()
    {
        // Nous appliquons la rotation de l'objet autour de l'axe Y en fonction de la vitesse définie.
        dnaTransform.Rotate(Vector3.up * speedRotation * Time.deltaTime, Space.Self);

        // Nous calculons la nouvelle position en Y en ajoutant un mouvement sinusoïdal.
        tempPosition = positionOffset;
        tempPosition.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude; // Nous appliquons l'oscillation verticale.

        // Nous mettons à jour la position de l'objet, en gardant la position X et Z d'origine.
        dnaTransform.position = new Vector3(tempPosition.x, tempPosition.y, tempPosition.z);
    }

}