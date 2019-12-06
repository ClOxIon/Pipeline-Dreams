using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;
    private void Awake() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<EntityDeath>().OnEntityDeath += EndGame; 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void EndGame(Entity Player) {
        FindObjectOfType<PlayerInputBroadcaster>().SetPlayerInputEnabled(PlayerInputFlag.GAMEOVER, false);
        GameOverPanel.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
