using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDetectorController : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject _metalDecetorHead;
    [SerializeField] private LayerMask _searchableLayerMask;
    [SerializeField] private AudioSource _audioSource;

    [Header("Settings")]
    [SerializeField] private bool _isOn;
    public bool IsOn { get { return _isOn; } private set { _isOn = value; } }
    [SerializeField] private float _checkSpeed;
    [SerializeField] private float _largeDistanceCheck;
    [SerializeField] private float _mediumDistanceCheck;
    [SerializeField] private float _shortDistanceCheck;

    [Header("Audio")]
    [SerializeField] private AudioClip _largeDistanceClip;
    [SerializeField] private AudioClip _mediumDistanceClip;
    [SerializeField] private AudioClip _shortDistanceClip;

    private Coroutine searchCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        IsOn = true;
        ToggleMetalDetector();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            IsOn = !IsOn;
            ToggleMetalDetector();
        }
    }

    private void ToggleMetalDetector()
    {
        if(IsOn)
        {
            searchCoroutine = StartCoroutine(MetalDectorSearch());
        }
        else
        {
            StopCoroutine(searchCoroutine);
        }
    }

    private IEnumerator MetalDectorSearch()
    {
        AudioClip currentClip;
        while(IsOn)
        {
            if (MetalDecetorSearch(_shortDistanceCheck))
            {
                currentClip = _shortDistanceClip;
            }
            else if(MetalDecetorSearch(_mediumDistanceCheck))
            {
                currentClip = _mediumDistanceClip;
            }
            else if(MetalDecetorSearch(_largeDistanceCheck))
            {
                currentClip = _largeDistanceClip;
            }
            else
            {
                currentClip = null;
            }
            
            if(!_audioSource.isPlaying && currentClip != null)
                _audioSource.PlayOneShot(currentClip);
            
            yield return new WaitForSeconds(_checkSpeed);
        }
    }

    private bool MetalDecetorSearch(float distance)
    {        
        Collider2D[] searchables = Physics2D.OverlapCircleAll(_metalDecetorHead.transform.position, distance, _searchableLayerMask);
        
        bool foundSearchable = searchables.Length > 0 ? true : false ;

        foreach(Collider2D searchable in searchables)
        {
            Searchable s = searchable.GetComponent<Searchable>();
            if (s == null) continue;

            s.UncoverSearchable();
        }

        return foundSearchable;
    }
}
