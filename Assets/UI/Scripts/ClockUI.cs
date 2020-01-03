using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class ClockUI : MonoBehaviour {
        [SerializeField] Image Fill;
        [SerializeField] GameObject Needle;
        [SerializeField] GameObject Hourglass;
        TaskManager CM;
        float targetPos = 0;
        float currentPos = 0;
        bool enemyTurnTrigger = false;
        [Range(0, 1000)] [SerializeField] float speed;
        private void Awake() {
            CM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<TaskManager>();
            CM.OnClockModified += (f) => { targetPos = f; };
            //CM.OnPlayerTurnEnd += () => { Hourglass.SetActive(true); Fill.enabled = false; Needle.SetActive(false); };
            //Hourglass.GetComponent<Animator>().Play("HourglassRotation", -1, 0f);
            //CM.OnPlayerTurnStart += () => { Hourglass.SetActive(false); Fill.enabled = true; Needle.SetActive(true); };
        }
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (currentPos + speed * Time.deltaTime < targetPos)
                currentPos += speed * Time.deltaTime;
            else
                currentPos = targetPos;
            Needle.transform.localRotation = Quaternion.Euler(0, 0, -360f * currentPos); Fill.fillAmount = currentPos - (int)(currentPos);
        }
    }
}