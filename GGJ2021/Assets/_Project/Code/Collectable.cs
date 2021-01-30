using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioClip[] _collectionSounds;    
    [SerializeField] private int _valueAmount;
    public int ValueAmount { get { return _valueAmount; } private set { _valueAmount = value; } }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Collect()
    {
        int rand = Random.Range(0, _collectionSounds.Length);
        _audioSource.PlayOneShot(_collectionSounds[rand]);
        _collider2D.enabled = false;
        _spriteRenderer.enabled = false;
        Destroy(gameObject, _collectionSounds[rand].length);
    }
}