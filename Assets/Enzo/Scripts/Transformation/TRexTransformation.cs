using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexTransformation : MonoBehaviour
{
    [Header("Set up")]
    [SerializeField] uint scoreToLoose = 2;

    [Header("Data")]
    bool isTransform = false;

    Scoring scoring;

    void Start()
    {
        scoring = GameObject.FindWithTag("Scoring").GetComponent<Scoring>();
    }

    void Update()
    {
        scoring.timer += Time.deltaTime;

        if (scoring.timer >= scoring.timeToGainScore && isTransform)
        {
            scoring.score -= scoreToLoose;
            scoring.timer = 0f;
        }
    }

    public void TRexTransfo()
    {
        isTransform = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Collider") && isTransform)
        {
            Destroy(other.gameObject);
        }
    }
}
