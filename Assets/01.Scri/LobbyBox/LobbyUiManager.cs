
using UnityEngine;



public class LobbyUiManager : MonoBehaviour
{
    [SerializeField] protected GameObject dim;
    protected GameObject dimClone = null;
    virtual public void Start() { }
    virtual public void  OpenPanel()
    {
        if (dimClone != null) return;
        dimClone = Instantiate(dim,this.transform);
        dimClone.transform.SetAsFirstSibling();
    }

    virtual public void ExitPanel()
    {
      if(dimClone != null)
        {
            Destroy(dimClone);
            dimClone = null;
        }
    }

    

}




