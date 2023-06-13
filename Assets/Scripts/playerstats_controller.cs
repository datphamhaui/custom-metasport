using UnityEngine;
using TMPro;
using Fusion;

public class playerstats_controller : NetworkBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject window;
    [SerializeField] GameObject username;
    [SerializeField] NetworkObject playerPV;

    public SkinnedMeshRenderer top_wear, bottom_wear, shoes;

    [Networked]
    public int top_wear_material { get; set; }
    [Networked]
    public int bottom_wear_material { get; set; }
    [Networked]
    public int shoes_material { get; set; }

    public int i, j, k;
    public Material[] myColors;
    // Start is called before the first frame update

    void Start()
    {
        top_wear_material = PlayerPrefs.GetInt("Top_wear");
        bottom_wear_material = PlayerPrefs.GetInt("Bottom_wear");
        shoes_material = PlayerPrefs.GetInt("Shoes");
        i = j = k = 0;
        username.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("PrefabName");

        username.SetActive(false);
        RPC_SetSkinn(top_wear_material, bottom_wear_material, shoes_material);
        window.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        top_wear_material = PlayerPrefs.GetInt("Top_wear");
        bottom_wear_material = PlayerPrefs.GetInt("Bottom_wear");
        shoes_material = PlayerPrefs.GetInt("Shoes");
        RPC_SetSkinn(top_wear_material, bottom_wear_material, shoes_material);
    }
    private void OnMouseDown()
    {
        if (!Object.HasInputAuthority)
        {
            window.SetActive(true);
        }
    }

    public void TopIncrement()
    {
        i = IncrementIndex(i);

        top_wear.material = myColors[i];
    }
    public void TopDecrement()
    {
        i = DecrementIndex(i);

        top_wear.material = myColors[i];
    }
    public void BottomIncrement()
    {
        j = IncrementIndex(j);

        bottom_wear.material = myColors[j];
    }
    public void BottomDecrement()
    {
        j = DecrementIndex(j);

        bottom_wear.material = myColors[j];
    }
    public void ShoesIncrement()
    {
        k = IncrementIndex(k);

        shoes.material = myColors[k];
    }
    public void ShoesDecrement()
    {
        k = DecrementIndex(k);

        shoes.material = myColors[k];
    }

    int IncrementIndex(int x)
    {
        if (x == myColors.Length - 1) x = 0;

        else x++;
        return x;
    }

    int DecrementIndex(int x)
    {
        if (x == 0) x = myColors.Length - 1;

        else x--;

        return x;
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_SetSkinn(int a, int b, int c)
    {
        top_wear.material = myColors[a];
        bottom_wear.material = myColors[b];
        shoes.material = myColors[c];
    }



}