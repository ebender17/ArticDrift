using UnityEngine;

public class GameController : MonoBehaviour
{
    public int collectableCount { get; private set; } = 0;
    public const int MAX_HEALTH = 100;
    public const int FALL_DAMAGE = 20;
    public const float SCENE_MIN_HEIGHT = -50f; //used to check if player has fallen off platform
    private int _currentHealth;
    private float _lastDamageTime;
    public float damageBufferTime = 1f;

    private Vector3 _currentCheckpoint;
    public Vector3 CurrentCheckpoint
    {
        get { return _currentCheckpoint; }
        set { _currentCheckpoint = value; }
    }

    [Header("Broadcasting on")]
    //Listening in UI Manager
    [SerializeField] private IntEventChannelSO _changeScoreUIEvent;
    [SerializeField] private IntEventChannelSO _changeHealthUIEvent;
    //did not make seperate GameManager b/c small scope of project
    [SerializeField] private GameResultChannelSO _gameResultEvent; //picked up by UI Manager
    [SerializeField] private VoidEventChannelSO _loadLevelEvent;

    private void Awake()
    {
        _currentHealth = MAX_HEALTH;
        _changeHealthUIEvent.RaiseEvent(_currentHealth);
    }
    private void Start()
    {
        _currentCheckpoint = transform.position;

    }
    private void Update()
    {
        //CheckPlayerPosition();
    }

    public void PlayerFallRespawn()
    {
        TakeDamage(FALL_DAMAGE);

        if(_currentHealth > 0)
            LoadLastCheckPoint(); //TODO: Checkpoint system and camera smooth     
    }

    private void LoadLastCheckPoint()
    {
        Debug.Log("Inside load last checkpoint" + _currentCheckpoint);
        transform.position = _currentCheckpoint;
    }


    private void TakeDamage(int damage)
    {
        if(Time.time >= _lastDamageTime + damageBufferTime)
        {
            _lastDamageTime = Time.time;

            Debug.Log("Player taken damage!");

            _currentHealth -= damage;

            _changeHealthUIEvent.RaiseEvent(_currentHealth);
        }

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        _gameResultEvent.RaiseEvent(false, collectableCount);
        
        //TODO: Start Courtine to load end scene
        Destroy(gameObject);
    }

    public void LevelComplete()
    {
        _loadLevelEvent.RaiseEvent(); //picked up by UI manager to display level complete UI before moving to next scene
        
        //TODO: Start Courotine to load next scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Collectable")
        {
            collectableCount++;
            _changeScoreUIEvent.RaiseEvent(collectableCount);

            Destroy(other.gameObject);
        }
    }

}
