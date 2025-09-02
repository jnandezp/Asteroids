using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField]
   private int lives = 3;
   
   [SerializeField]
   private float respawnTime = 3.0f;
   
   public static GameManager Instance; // acceso global
   
   public ParticleSystem explosion;

   public ScoreManager _scoreManager;
   
   public Player player;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         DontDestroyOnLoad(gameObject); // opcional, si quieres que persista entre escenas
      }
      else
      {
         Destroy(gameObject);
      }
   }

   public void AsteroidDestroyed(Asteroid asteroid)
   {
      this.explosion.transform.position = asteroid.transform.position;
      this.explosion.Play();
      
      _scoreManager.AddScore(asteroid.GetPoints());
   }

   private void Respawn()
   {
      player.Respawn(Vector3.zero);
      player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
      Invoke(nameof(TurnOnCollisions), 3.0f);
   }

   private void TurnOnCollisions()
   {
      this.player.gameObject.layer = LayerMask.NameToLayer("Player");
   }

   private void GameOver()
   {
      this.lives = 3;
      
      Invoke(nameof(Respawn), this.respawnTime);
   }
   
   private void OnEnable()
   {
      player.OnPlayerDied += HandlePlayerDeath;
   }

   private void OnDisable()
   {
      player.OnPlayerDied -= HandlePlayerDeath;
   }

   private void HandlePlayerDeath()
   {
      this.explosion.transform.position = player.transform.position;
      this.explosion.Play();
    
      this.lives--;

      if (this.lives <= 0)
      {
         GameOver();
      }
      else
      {
         Invoke(nameof(Respawn), this.respawnTime);   
      }
   }
}
