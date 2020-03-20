using UnityEngine;

namespace PipelineDreams {
    public class GameOver : MonoBehaviour {
        [SerializeField] GameObject GameOverPanel;
        [SerializeField] Entity.Entity Player;
        private void Awake() {
            Player.OnEntityDeath += EndGame;
        }
        // Start is called before the first frame update
        void Start() {

        }
        void EndGame(Entity.Entity Player) {
            FindObjectOfType<PlayerInputBroadcaster>().SetPlayerInputEnabled(PlayerInputFlag.GAMEOVER, false);
            GameOverPanel.SetActive(true);

        }
        // Update is called once per frame
        void Update() {

        }
    }
}