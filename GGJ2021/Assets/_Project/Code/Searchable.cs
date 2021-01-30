using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searchable : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _alphaIncrease;
    [SerializeField] private List<Collectable> _collectables;
    public List<Collectable> Collectables
    {
        get { return _collectables; }
        private set { _collectables = value; }
    }

    [SerializeField] private List<Transform> _spawnLocations;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Search()
    {
        int spawnLocation = 0;
        foreach(Collectable collectable in _collectables)
        {
            Instantiate(collectable, _spawnLocations[spawnLocation].position, Quaternion.identity);
            spawnLocation++;
        }
        Destroy(gameObject);
    }

    public void UncoverSearchable()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a +_alphaIncrease);
    }
}
