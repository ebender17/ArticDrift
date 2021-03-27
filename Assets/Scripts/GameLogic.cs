using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private int collectableCount = 0;
    public const int MAX_HEALTH = 100;
    public const int FALL_DAMAGE = 20;
    public const float SCENE_MIN_HEIGHT = -50f; //used to check if player has fallen off platform
    private int currentHealth;

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
    [SerializeField] private GameResultChannelSO _gameResultEvent;

    private void Awake()
    {
        currentHealth = MAX_HEALTH;
        _changeHealthUIEvent.RaiseEvent(currentHealth);
    }
    private void Start()
    {
        _currentCheckpoint = transform.position;

    }
    private void Update()
    {
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        if (transform.position.y < SCENE_MIN_HEIGHT)
        {
            TakeDamage(20);

            if(currentHealth > 0)
                LoadLastCheckPoint(); //TODO: Checkpoint system and camera smooth
        }
            
    }

    private void LoadLastCheckPoint()
    {
        transform.position = _currentCheckpoint;
    }


    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            _changeHealthUIEvent.RaiseEvent(currentHealth);
        }
    }

    private void Death()
    {
        _gameResultEvent.RaiseEvent(false, collectableCount);
        Destroy(gameObject);
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
