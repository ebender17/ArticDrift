using UnityEngine;

public class GameController : MonoBehaviour
{
    private CharacterController _cc;
    private PlayerController _playerController;
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

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        _playerController = GetComponent<PlayerController>();

        _currentHealth = MAX_HEALTH;
        _changeHealthUIEvent.RaiseEvent(_currentHealth);
        _currentCheckpoint = transform.position;

    }
    private void Update()
    {
        //CheckPlayerPosition();
    }

    public void PlayerFallRespawn()
    {
        TakeDamage(FALL_DAMAGE);

        if (_currentHealth > 0)
            LoadLastCheckPoint(); //TODO: Checkpoint system   
    }

    private void LoadLastCheckPoint()
    {
        //Character controller will ignore transform.position =, therefore I disabled and enaled it while loading checkpoint
        _cc.enabled = false;
        transform.position = _currentCheckpoint;
        _cc.enabled = true;

        _playerController.velocity = Vector3.zero;

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
        
        Destroy(gameObject);
    }

    public void LevelComplete()
    {
        _gameResultEvent.RaiseEvent(true, collectableCount);
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
