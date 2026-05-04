using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class SummoningDecision : MonoBehaviour
{
    BundlePackage turrents;
    Rigidbody rb;

    [SerializeField]bool placed = false;
    bool Summoning => SummonManager.Instance.summoning;

    [SerializeField] float checkRadius = 1.5f;
    [SerializeField] Vector3 offset;

    public UnityEvent eventRed;
    public UnityEvent eventGreen;

    bool debris;
    bool lastDebrisState;

    Coroutine routine;

    void Awake()
    {
        turrents = GetComponent<BundlePackage>();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        placed = false;
        routine = StartCoroutine(CheckRoutine());
        if (!placed) rb.position = SummonManager.Instance.platform.position + offset;
       
        eventRed.AddListener(SummonManager.Instance.CannotEndSummoning);
        eventGreen.AddListener(SummonManager.Instance.CanEndSummoning);
    }

    void OnDisable()
    {
        if (routine != null)
            StopCoroutine(routine);
    }

    IEnumerator CheckRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.33f);

        while (!placed)
        {
            CheckSphere();
            SummonMethod();
            yield return wait;
        }
    }

    public List<Collider> detectedObjects = new();

    void CheckSphere()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, checkRadius);
        detectedObjects.Clear();
        debris = false;

        foreach (var hit in hits)
        {
            if (IsIgnored(hit)) continue;
            if (hit.transform.IsChildOf(transform)) continue;
            detectedObjects.Add(hit);
            debris = true;
            break;
        }
    }

    bool IsIgnored(Collider other)
    {
        return other.TryGetComponent<Terrain>(out _) ||
               other.TryGetComponent<Tile>(out _) ||
               other.TryGetComponent<Trigger>(out _) ||
               other.TryGetComponent<Player>(out _) ||
               other.TryGetComponent<Axe>(out _)||
               other.TryGetComponent<Wall>(out _)||
               other.TryGetComponent<BaseEnemyAI>(out _);
    }

    void SummonMethod()
    {
        if (placed) return;

        if (Summoning)
        {
            rb.position = SummonManager.Instance.platform.position + offset;
            rb.rotation = SummonManager.Instance.platform.rotation;
             SetState(red: debris, green: !debris);
        }
        else
        {
            placed = true;
            SetState(normal: true);
        }

        if (!debris)
        {
            eventGreen?.Invoke();
        }

        if (debris != lastDebrisState)
        {
            if (debris)
                eventRed?.Invoke();
            else
                eventGreen?.Invoke();

            lastDebrisState = debris;
        }
    }

    void SetState(bool red = false, bool green = false, bool normal = false)
    {
        if (turrents.red.activeSelf != red) turrents.red.SetActive(red);
        if (turrents.green.activeSelf != green) turrents.green.SetActive(green);
        if (turrents.normal.activeSelf != normal) turrents.normal.SetActive(normal);
    }

    
}