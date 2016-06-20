using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {
	public GameObject jogarMenu;
	public GameObject quitMenu;
	public GameObject opcoesMenu;
	public GameObject mainMenu;
	private int volume;
	private int maxVolume = 100;
	private int minVolume = 0;

	void Start () {
		quitMenu.SetActive(false);
		jogarMenu.SetActive(false);
        opcoesMenu.SetActive(false);
        mainMenu.SetActive(true);

        volume =50;
	}
	
	public void ExitPress()
	{
        quitMenu.SetActive(true);
        jogarMenu.SetActive(false);
        opcoesMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
	
	public void NoPress()
	{
        quitMenu.SetActive(false);
        jogarMenu.SetActive(false);
        opcoesMenu.SetActive(false);
        mainMenu.SetActive(true);
	}
	
	public void StartLevel()
	{
        quitMenu.SetActive(false);
        jogarMenu.SetActive(true);
        opcoesMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

	public void opcoes(){
        quitMenu.SetActive(false);
        jogarMenu.SetActive(true);
        opcoesMenu.SetActive(false);
        mainMenu.SetActive(true);
	}

	public void aumenta(){
		if(volume<maxVolume){
			volume++;
		}
	}

	public void diminui(){

		if(volume>minVolume){
			volume--;
		}
	}
    
	public void NewGame() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
	}

	public void Continuar(){
        SceneManager.LoadScene(1);
    }

	public void voltar()
	{
        quitMenu.SetActive(false);
        jogarMenu.SetActive(false);
        opcoesMenu.SetActive(false);
        mainMenu.SetActive(true);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}