using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameState gameState;

    [SerializeField]
    private TextMeshProUGUI errorMessageText;

    public GameObject inputField; 



    private string inputText;
    // Start is called before the first frame update
    void Start()
    {
        errorMessageText.text = "";
        inputText = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        if (string.IsNullOrEmpty(inputText))
        {
            errorMessageText.text = "Player name cannot be empty";
        }
        else
        {
            if (inputText.Trim().Length == 0)
            {
                errorMessageText.text = "Player name cannot be empty";
            }
            else
            {
                gameState.playerName = inputText;
                SceneManager.LoadSceneAsync(1);
            }
        }
    }
    public void ReadPlayerInput(string s)
    {
        inputText = inputField.GetComponent<TMP_InputField>().text;
    }
}
