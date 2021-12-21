using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class IInventory : MonoBehaviour
{
    public ISlot[] inventory = new ISlot[15];
    public ISlot[] hotbar = new ISlot[5];

    public GameObject UIinv;
    public ISlot selectedSlot;
    public int slotinmano;

    [SerializeField]private Transform mano;
    [SerializeField]private Transform camerarotation;
    [SerializeField]private GameObject mapa;
    [SerializeField]private AudioClip[] sonidos;
    [SerializeField] private AudioSource reproductor;

    public GameObject ObjInMano;
    private IInventoryUI invUI;
    private IStatus status;
    private Manager manager;
    private ICrafting craft;
    private IData data;
    
    void Start()
    {
        invUI = FindObjectOfType<IInventoryUI>();
        status = GetComponent<IStatus>();
        manager = FindObjectOfType<Manager>();
        craft = FindObjectOfType<ICrafting>();
        mapa.SetActive(false);
        for (int i = 0; i < 15; i++)
        {
            inventory[i] = new ISlot(null,0);
        }
        data = FindObjectOfType<IData>();
        //data.LoadJSON();
        UIinv.SetActive(false);
        UpdateHotbar();
        invUI.UpdateInv();


    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !manager.menu.activeSelf)
        {
            reproductor.clip = sonidos[0];
            reproductor.Play();
            UIinv.SetActive(!UIinv.activeSelf);
            Time.timeScale = (UIinv.activeSelf || craft.craftMenu.activeSelf) ? 0 : 1;
            Cursor.lockState =  (UIinv.activeSelf || craft.craftMenu.activeSelf) ? CursorLockMode.None : CursorLockMode.Locked;
        }
        if (!UIinv.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) PonerEnMano(0);

            if (Input.GetKeyDown(KeyCode.Alpha2)) PonerEnMano(1);

            if (Input.GetKeyDown(KeyCode.Alpha3)) PonerEnMano(2);

            if (Input.GetKeyDown(KeyCode.Alpha4)) PonerEnMano(3);

            if (Input.GetKeyDown(KeyCode.Alpha5)) PonerEnMano(4);

            if (Input.GetMouseButtonDown(1) && ObjInMano != null &&ObjInMano.activeSelf && ObjInMano.GetComponent<IObject>().uso != null)
                Usos(ObjInMano.GetComponent<IObject>().uso);
        }
    }

    private void PonerEnMano(int i)
    {
        if (hotbar[i].quantity != 0)
        {
            if(ObjInMano != null)ObjInMano.SetActive(false);
            ObjInMano = hotbar[i].prefab;
            invUI.hotbar[slotinmano].GetComponent<CanvasGroup>().alpha = 0.8f;
            slotinmano = i;
            invUI.hotbar[i].GetComponent<CanvasGroup>().alpha = 1;
            ObjInMano.transform.localPosition = hotbar[i].obj.posicionMano;
            ObjInMano.transform.localRotation = hotbar[i].obj.rotacionMano;
            ObjInMano.GetComponent<IObject>().inMano = true;
            ObjInMano.SetActive(true);
            ObjInMano.GetComponent<Rigidbody>().isKinematic = true;
            if(ObjInMano.TryGetComponent<BoxCollider>(out BoxCollider col))  col.enabled = false;

            //checkear si es  un arma
            if (hotbar[i].obj.IsArma)
            {
                //si es arma cambiar modo de ataque
                GetComponent<ThirdPersonController>().ArmaEnMano = true;
                GetComponent<ThirdPersonController>().ArmaCollider = ObjInMano.GetComponent<IObject>().AttackArea;

            } else GetComponent<ThirdPersonController>().ArmaEnMano = false;

        }
        else
        {
            if(ObjInMano!=null) ObjInMano.SetActive(false);
            GetComponent<ThirdPersonController>().ArmaEnMano = false;
            invUI.hotbar[slotinmano].GetComponent<CanvasGroup>().alpha = 0.8f;
            slotinmano = i;
            invUI.hotbar[i].GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    //los primeros 5 slots del inventario son de la hotbar
    void UpdateHotbar()
    {
        for (int i = 0; i < hotbar.Length; i++)  hotbar[i] = inventory[i];
        PonerEnMano(slotinmano);
        
    }
    int BuscarVacio() //busca el primer slot vacio, si no encuentra devuelve -1
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].quantity == 0) return i;
        }
        Debug.Log("inventario lleno");
        return -1;
    }
    public bool Add(IObject item)
    {
        bool agregado = false;

        foreach (ISlot s in inventory)
        {
            if (s.quantity != 0 && s.obj.name == item.name && item.stackable > s.quantity)
            {
                s.quantity++;
                agregado = true;
                break;
            }
        }
        
        if (!agregado)
        {
            int i = BuscarVacio();
            if (i != -1)
            {
                inventory[i] = new ISlot(item, 1);
                Transform padre = inventory[i].obj.recogible ? mano : camerarotation;
                inventory[i].prefab = Instantiate(inventory[i].obj.prefab,padre.position, padre.rotation,padre);
                inventory[i].prefab.SetActive(false);
                agregado = true;
            }
        }
        UpdateHotbar();
        invUI.UpdateInv();
        if(agregado)
        {
            reproductor.clip = sonidos[3];
            reproductor.Play();
        }
        return agregado;
    }
    public int BuscarSlot(ISlot slot)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == slot) return i;
        }
        Debug.Log("no se encontro el slot");
        return -1;
    }
    public bool Check(string name, int quantity)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].quantity >= quantity && inventory[i].obj.name == name) return true;
        }
        return false;
    }
    public int FindID(string name, int quantity)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].quantity >= quantity && inventory[i].obj.name == name) return i;
        }
        return -1;
    }
    public void CambiarLugar( int dest)
    {
        ISlot tempslot = (inventory[dest].quantity == 0) ? new ISlot(null, 0) : inventory[dest];
        int i = BuscarSlot(selectedSlot);
        inventory[dest] = inventory[i];
        inventory[i] = tempslot;
        UpdateHotbar();
        invUI.UpdateInv();
    }
    public void DropSelected()
    {
        int i = BuscarSlot(selectedSlot);
        for (int j = 0; j < selectedSlot.quantity; j++)
        {
            if (ObjInMano.TryGetComponent<BoxCollider>(out BoxCollider col)) col.enabled = true;
            GameObject droped = Instantiate(selectedSlot.prefab, selectedSlot.prefab.transform.position + new Vector3(2,j/2+1,0), selectedSlot.prefab.transform.rotation);
            droped.SetActive(true);
            droped.GetComponent<Rigidbody>().isKinematic = false;
            droped.GetComponent<IObject>().inMano = false;
        }
        if (i > -1)
        {
            Destroy(inventory[i].prefab);
            inventory[i] = new ISlot(null, 0);
        }
        selectedSlot = null;
        UpdateHotbar();
        invUI.UpdateInv();
    }
    public void DropObject(IObject obj)
    {
        GameObject droped = Instantiate(obj.prefab, transform.position + new Vector3(2, 1, 0), transform.rotation);
        droped.GetComponent<Rigidbody>().isKinematic = false;
        droped.GetComponent<IObject>().inMano = false;
    }
    public void sacaruno(int i) 
    { 
        inventory[i].quantity--;
        UpdateHotbar();
        invUI.UpdateInv();
    }
    public void RemoveAll(IQuerry[] querries)
    {
        foreach (IQuerry querry in querries)
        {
            int j = FindID(querry.name, querry.quantity);
            for (int i = 0; i < querry.quantity; i++)
            {
                sacaruno(j);
            }
        }
    }
    public bool QuerryRemove(IQuerry[] querries)
    {
        foreach(IQuerry querry in querries)
        {
            if (!Check(querry.name, querry.quantity)) return false;
        }
        RemoveAll(querries);
        return true;
    }
    public bool QuerryCheck(IQuerry[] querries)
    {
        foreach (IQuerry querry in querries)
        {
            if (!Check(querry.name, querry.quantity)) return false;
        }
        return true;
    }
    public void Usos(string uso)
    {
        switch (uso)
        {
            case "naranja":
                reproductor.clip = sonidos[1];
                reproductor.Play();
                sacaruno(slotinmano);
                status.Comer(15);
                status.Beber(15);
                break;
            case "manzana":
                reproductor.clip = sonidos[1];
                reproductor.Play();
                sacaruno(slotinmano);
                status.Comer(20);
                status.Beber(10);
                break;
            case "pan":
                reproductor.clip = sonidos[1];
                reproductor.Play();
                sacaruno(slotinmano);
                status.Comer(30);
                break;
            case "primerosauxilios":
                sacaruno(slotinmano);
                status.Curar(100);
                break;
            case "meat":
                reproductor.clip = sonidos[1];
                reproductor.Play();
                sacaruno(slotinmano);
                status.Comer(50);
                break;
            case "cantimplora":
                reproductor.clip = sonidos[2];
                reproductor.Play();
                sacaruno(slotinmano);
                status.Beber(100);
                break;
            case "botella":
                reproductor.clip = sonidos[2];
                reproductor.Play();
                sacaruno(slotinmano);
                status.Beber(40);
                break;
            case "mapa":
                mapa.SetActive(!mapa.activeSelf);
                break;
            case "build":
                ObjInMano.transform.SetParent(null);
                ObjInMano.GetComponent<Rigidbody>().isKinematic = false;
                ObjInMano = null;
                sacaruno(slotinmano);
                break;
            default:
                break;
        }
    }

}
[System.Serializable]
public class IQuerry
{
    public string name;
    public int quantity;
    public Sprite icon;

    public IQuerry(string name, int quantity)
    {
        this.name = name;
        this.quantity = quantity;
    }
}
[System.Serializable]
public class ISlot
{
    public IObject obj { set; get; }
    public int quantity { set; get; }

    public GameObject prefab;

    public ISlot(IObject item, int cantidad)
    {
        obj = item;
        quantity = cantidad;
    }

}


