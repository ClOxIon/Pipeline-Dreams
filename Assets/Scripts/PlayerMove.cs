using System;
using UnityEngine;

namespace PipelineDreams {
    public class PlayerMove : MonoBehaviour {
        /// <summary>
        /// LH vector
        /// </summary>
        public event Action<Vector3Int> OnPlayerTranslation;
        public event Action OnAfterPlayerTranslation;
        /// <summary>
        /// LHQ of the camera.
        /// </summary>
        public event Action<Quaternion> OnPlayerRotation;

        /// <summary>
        /// l,u,r,d, space
        /// </summary>
        public event Action<Command> OnCommandKeyPressed;
        [SerializeField] TaskManager CM;

        [SerializeField] Entity.Entity Player;

        private void Awake() {

            FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);

        }
        // Start is called before the first frame update
        void Start() {
        }


        void PlayerRotate(Quaternion deltaQ) {

            Player.GetComponent<Entity.Move>().Face(Util.QToFace(Player.IdealRotation * deltaQ), CM.Clock);


        }

        void PlayerTranslateForward() {
            Player.GetComponent<Entity.Move>().MoveToward(Util.QToUVector(Player.IdealRotation), CM.Clock);

        }



        /// <summary>
        /// These are using message sent by PlayerInput.
        /// </summary>
        /// <param name="value"></param>
        private void OnMoveForward(object value) {
            if (Player.GetComponent<Entity.Move>().CanMove(Player.IdealPosition+Util.QToUVector(Player.IdealRotation))) {

                PlayerTranslateForward();
                OnCommandKeyPressed(Command.space);
            }
        }
        private void OnTurnLeft(object value) {
            if (Player.GetComponent<Entity.Move>().CanRotate(Player.IdealRotation * Util.TurnLeft)) {
                PlayerRotate(Util.TurnLeft);
                OnSightTurnLeft(null);
                OnCommandKeyPressed(Command.rotate);
            }
        }
        private void OnTurnRight(object value) {
            if (Player.GetComponent<Entity.Move>().CanRotate(Player.IdealRotation * Util.TurnRight)) {
                PlayerRotate(Util.TurnRight);
                OnSightTurnRight(null);
                OnCommandKeyPressed(Command.rotate);
            }
        }
        private void OnTurnUp(object value) {
            if (Player.GetComponent<Entity.Move>().CanRotate(Player.IdealRotation * Util.TurnUp)) {
                PlayerRotate(Util.TurnUp);
                OnSightTurnUp(null);
                OnCommandKeyPressed(Command.rotate);
            }
        }
        private void OnTurnDown(object value) {
            if (Player.GetComponent<Entity.Move>().CanRotate(Player.IdealRotation * Util.TurnDown)) {
                PlayerRotate(Util.TurnDown);
                OnSightTurnDown(null);
                OnCommandKeyPressed(Command.rotate);
            }
        }
        private void OnWait(object value) {
            Player.GetComponent<Entity.PlayerAI>().EntityClock += 1f;
        }
        
        private void OnSightTurnLeft(object value)
        {
            var sr = Player.GetComponent<Entity.SightWithRotation>();
            sr.Face(Util.QToFace(sr.IdealRotation * Util.TurnLeft), CM.Clock);
        }
        private void OnSightTurnRight(object value)
        {
            var sr = Player.GetComponent<Entity.SightWithRotation>();
            sr.Face(Util.QToFace(sr.IdealRotation * Util.TurnRight), CM.Clock);
        }
        private void OnSightTurnUp(object value)
        {
            var sr = Player.GetComponent<Entity.SightWithRotation>();
            sr.Face(Util.QToFace(sr.IdealRotation * Util.TurnUp), CM.Clock);
        }
        private void OnSightTurnDown(object value)
        {
            var sr = Player.GetComponent<Entity.SightWithRotation>();
            sr.Face(Util.QToFace(sr.IdealRotation*Util.TurnDown), CM.Clock);
        }
    }
}