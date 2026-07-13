
using UnityEngine;



public class LobbyUiManager : MonoBehaviour
{
    [SerializeField] protected GameObject dim;
    virtual public void Start()
    {
        
    }

    virtual public void  OpenPanel()
    {
        Instantiate(dim);
    }

    virtual public void ExitPanel()
    {
        Destroy(dim);
    }

    

}




