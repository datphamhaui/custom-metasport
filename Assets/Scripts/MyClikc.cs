using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StarterAssets;

public class MyClikc : MonoBehaviour
{
    // Start is called before the first frame update
    public Button nextButton;
	
	public Button previousButton;
	
	public Button loadButton;

	public Button nextButtonTop;

	public Button nextButtonBottom;

	public Button nextButtonShoes;

	public Button previousButtonTop;

	public Button previousButtonBottom;

	public Button previousButtonShoes;

	public playerstats_controller pc;
	
	public GameObject[] myPrefab;
	
	public GameObject NickName;
	
	private int x = 0;
	
	GameObject player;
	
	TextMeshProUGUI mText;

	void Start () {

		Cursor.lockState = CursorLockMode.None;
		
		Cursor.visible = true;

		mText = NickName.GetComponent<TextMeshProUGUI>();
		
		player = Instantiate(myPrefab[0], new Vector3(883.57f, 0.58f, -9.29f), Quaternion.identity);
		
		pc = player.GetComponent<playerstats_controller>();

		foreach (Transform child in player.transform)
        {
			child.gameObject.SetActive(false);
        }
		
		player.transform.GetChild(0).gameObject.SetActive(true);
		
		player.transform.GetChild(1).gameObject.SetActive(true);
		
		player.GetComponent<ThirdPersonController>().enabled=false;
		
		player.GetComponent<playerstats_controller>().enabled = false;
		
		player.GetComponent<Mouse_highlighter>().enabled = false;

		player.GetComponent<push2talk>().enabled = false;

		player.transform.Rotate(new Vector3(0, 180, 0));

		PlayerPrefs.SetString("PrefabName", myPrefab[x].name);
		
		nextButton.onClick.AddListener(ShowNextCharacter);
		
		previousButton.onClick.AddListener(ShowPreviousCharacter);
		
		loadButton.onClick.AddListener(SceneLoader);

		nextButtonTop.onClick.AddListener(pc.TopIncrement);

		previousButtonTop.onClick.AddListener(pc.TopDecrement);

		nextButtonBottom.onClick.AddListener(pc.BottomIncrement);

		previousButtonBottom.onClick.AddListener(pc.BottomDecrement);

		nextButtonShoes.onClick.AddListener(pc.ShoesIncrement);

		previousButtonShoes.onClick.AddListener(pc.ShoesIncrement);

		mText.SetText(myPrefab[x].name);
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		
	}
    void ShowNextCharacter(){

		Destroy(player);
		
		x = (x + 1) % myPrefab.Length;

		mText.SetText(myPrefab[x].name);

		player = Instantiate(myPrefab[x], new Vector3(883.57f, 0.58f, -9.29f), Quaternion.identity);
		
		pc = player.GetComponent<playerstats_controller>();
		
		player.GetComponent<Mouse_highlighter>().enabled = false;
		
		foreach (Transform child in player.transform)
		{
			child.gameObject.SetActive(false);
		}
		
		player.transform.GetChild(0).gameObject.SetActive(true);
		
		player.transform.GetChild(1).gameObject.SetActive(true);

		player.GetComponent<ThirdPersonController>().enabled = false;

		player.GetComponent<playerstats_controller>().enabled = false;

		player.GetComponent<Mouse_highlighter>().enabled = false;

		player.GetComponent<push2talk>().enabled = false;

		player.transform.Rotate(new Vector3(0, 180, 0));

		nextButtonTop.onClick.AddListener(pc.TopIncrement);

		previousButtonTop.onClick.AddListener(pc.TopDecrement);

		nextButtonBottom.onClick.AddListener(pc.BottomIncrement);

		previousButtonBottom.onClick.AddListener(pc.BottomDecrement);

		nextButtonShoes.onClick.AddListener(pc.ShoesIncrement);

		previousButtonShoes.onClick.AddListener(pc.ShoesIncrement);

	}

	void ShowPreviousCharacter()
	{
		Destroy(player);

		x = x - 1;
		if(x < 0)
        {
			x = myPrefab.Length - 1;
		}

		mText.SetText(myPrefab[x].name);

		player = Instantiate(myPrefab[x], new Vector3(883.57f, 0.58f, -9.29f), Quaternion.identity);
		
		foreach (Transform child in player.transform)
		{
			child.gameObject.SetActive(false);
		}

		player.transform.GetChild(0).gameObject.SetActive(true);
		
		player.transform.GetChild(1).gameObject.SetActive(true);

		player.GetComponent<ThirdPersonController>().enabled = false;

		player.GetComponent<playerstats_controller>().enabled = false;

		player.GetComponent<Mouse_highlighter>().enabled = false;

		player.GetComponent<push2talk>().enabled = false;

		player.transform.Rotate(new Vector3(0, 180, 0));

	}

	void SceneLoader()
    {
		PlayerPrefs.SetString("PrefabName", myPrefab[x].name);

		PlayerPrefs.SetInt("Top_wear", pc.i);

		PlayerPrefs.SetInt("Bottom_wear", pc.j);

		PlayerPrefs.SetInt("Shoes", pc.k);

		PlayerPrefs.Save();
		
		SceneManager.LoadScene("GameFusion");

	}

	
	
	/*void OnApplicationFocus(bool hasFocus)
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}*/

}

