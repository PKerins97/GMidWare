using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Windows.Speech;
public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;


    private void Start()
    {
        keywordActions.Add("fire", Fire);
        keywordActions.Add("turn left", TurnLeft);
        keywordActions.Add("turn right", TurnRight);
        keywordActions.Add("turn up", TurnUp);
        keywordActions.Add("turn down", TurnDown);

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void TurnRight()
    {
        Transform rotaterTransform = transform.transform.Find("rotater");
        rotaterTransform.Rotate(0f, 20f, 0);
    }

    private void TurnLeft()
    {
        Transform rotaterTransform = transform.transform.Find("rotater");
        rotaterTransform.Rotate(0f, -20f, 0);
    }

    private void TurnUp()
    {
        Transform rotaterTransform = transform.transform.Find("rotater");
        Transform cannonTransform = rotaterTransform.transform.Find("cannon");
        cannonTransform.Rotate(-10f, 0, 0);
    }

    private void TurnDown()
    {
        Transform rotaterTransform = transform.transform.Find("rotater");
        Transform cannonTransform = rotaterTransform.transform.Find("cannon");
        cannonTransform.Rotate(10f, 0, 0);
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.transform.forward * 5000f);

        Debug.Log("Boom");
    }
}
