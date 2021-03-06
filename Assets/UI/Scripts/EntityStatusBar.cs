﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class EntityStatusBar : MonoBehaviour {
        [SerializeField] GameObject Panel;
        [SerializeField] Image Emoji;
        [SerializeField] Sprite Surprised;
        [SerializeField] Sprite Confused;
        [SerializeField] Sprite None;
        [SerializeField] Sprite Angry;

        public event Action OnInit;
        public Entity.Entity entity { get; private set; }
        Transform eT;
        Transform Reference;//The reference transform to show the status bar.
        Transform t;
        public void Init(Entity.Entity e) {
            Reference = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Entity.SightWithRotation>().SightTransform;
            t = transform;
            entity = e;
            eT = entity.transform;
            OnInit?.Invoke();
        }
        private void Awake() {


        }
        public bool Visible { get; private set; }
        // Start is called before the first frame update
        void Start() {

        }
        public void Show(bool b) {
            Visible = b;
            Panel.SetActive(b);

        }

        // Update is called once per frame
        void Update() {
            if (Visible) {
                if(entity.GetComponent<Entity.AI>()!=null)
                    
                switch (entity.GetComponent<Entity.AI>().State) {
                case Entity.AIState.Attack:
                    Emoji.sprite = Angry;
                    Emoji.color = Color.red;
                    break;
                case  Entity.AIState.Wander:
                    Emoji.sprite = None;
                    Emoji.color = new Color(0, 0, 0, 0);
                    break;
                case Entity.AIState.Chase:
                    Emoji.sprite = Surprised;
                    Emoji.color = Color.yellow;
                    break;
                case Entity.AIState.Confused:
                    Emoji.sprite = Confused;
                    Emoji.color = Color.green;
                    break;
                }
                t.position = eT.position;
                t.LookAt(Reference, Reference.up);
            }
        }
    }
}