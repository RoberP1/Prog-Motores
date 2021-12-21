using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ICrafting : MonoBehaviour
{
    
    private IInventory inventory;
    private Manager manager;


    public GameObject craftMenu;
    [SerializeField] private GameObject craftprefab;
    [SerializeField] private List<IRecipe> recipes;
    public AudioSource reproductor;
    public AudioClip sonido;

    void Start()
    {
        manager = FindObjectOfType<Manager>();
        inventory = FindObjectOfType<IInventory>();
        craftMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !manager.menu.activeSelf)
        {
            craftMenu.SetActive(!craftMenu.activeSelf);
            Synchronize();
            Time.timeScale = (craftMenu.activeSelf || inventory.UIinv.activeSelf) ? 0 : 1;
            Cursor.lockState = (craftMenu.activeSelf || inventory.UIinv.activeSelf) ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
    private void Craft(IRecipe recipe)
    {
        print("se intenta craftear");
        if (inventory.QuerryRemove(recipe.components))
        {
            if (!inventory.Add(recipe.result)) inventory.DropObject(recipe.result);
            reproductor.clip = sonido;
            reproductor.Play();
        }
            
        Synchronize();
    }
    public void Synchronize()
    {
        foreach (Transform child in craftMenu.transform) if (child.gameObject != craftprefab) Destroy(child.gameObject);
        foreach (IRecipe recipe in recipes)
        {
            if (inventory.QuerryCheck(recipe.components))
            {
                Transform newrecipe = Instantiate(craftprefab, craftMenu.transform).transform;
                newrecipe.gameObject.SetActive(true);
                
                Transform icon1 = newrecipe.Find("Icon1");
                icon1.GetComponent<Image>().sprite = recipe.components[0].icon;
                icon1.GetComponentInChildren<Text>().text = "x" + recipe.components[0].quantity;
                if (recipe.components.Length > 1)
                {
                    Transform icon2 = newrecipe.Find("Icon2");
                    newrecipe.Find("Suma").gameObject.SetActive(true);
                    icon2.gameObject.SetActive(true);
                    icon2.GetComponent<Image>().sprite = recipe.components[1].icon;
                    icon2.GetComponentInChildren<Text>().text = "x" + recipe.components[1].quantity;
                }
                Transform result = newrecipe.Find("result");
                print(recipe);
                result.GetComponent<Image>().sprite = recipe.result.icon.sprite;
                result.GetComponent<Button>().onClick.AddListener(() => Craft(recipe));
            }

        }
    }
}
