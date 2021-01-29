using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searchable : MonoBehaviour
{
    [SerializeField] private List<Collectable> _collectables;
    public List<Collectable> Collectables
    {
        get { return _collectables; }
        private set { _collectables = value; }
    }

    [SerializeField] private List<Transform> _spawnLocations;

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
}
