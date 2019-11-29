using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityStatusBar : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] Image Emoji;
    [SerializeField] Sprite Surprised;
    [SerializeField] Sprite Confused;
    [SerializeField] Sprite None;
    [SerializeField] Sprite Angry;

    public event Action OnInit;
    public Entity entity { get; private set; }
    Transform eT;
    Transform player;
    Transform t;
    public void Init(Entity e) {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        t = transform;
        entity = e;
        eT = entity.transform;
        OnInit?.Invoke();
    }
    private void Awake() {
        

    }
    public bool Visible { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Show(bool b) {
        Visible = b;
        Panel.SetActive(b);

    }

    // Update is called once per frame
    void Update()
    {
        if (Visible) {
            switch (entity.GetComponent<EntityAI>().state) {
            case EntityAIState.Attack:
                Emoji.sprite = Angry;
                Emoji.color = Color.red;
                break;
            case EntityAIState.Wander:
                Emoji.sprite = None;
                Emoji.color = new Color(0, 0, 0, 0);
                break;
            case EntityAIState.Chase:
                Emoji.sprite = Surprised;
                Emoji.color = Color.yellow;
                break;
            case EntityAIState.Confused:
                Emoji.sprite = Confused;
                Emoji.color = Color.green;
                break;
            }
           t.position = eT.position;
           t.LookAt(player,player.up);
        }
    }
}
