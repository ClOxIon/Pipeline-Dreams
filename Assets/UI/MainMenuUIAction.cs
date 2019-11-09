using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIAction : MonoBehaviour
{
    [SerializeField] GameObject ContinueButton;
    // Start is called before the first frame update
    void Start()
    {
        ContinueButton.SetActive(GameManager.Instance.IsSaveFileExist());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButtonClicked() {
        GameManager.Instance.StartNewGame();
    }
    public void ContinueButtonClicked() {
        GameManager.Instance.LoadGame();

    }
}
