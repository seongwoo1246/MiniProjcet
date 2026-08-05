using UnityEngine;



public class MultiStart : MonoBehaviour
{

    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera SecendCamera;
    [SerializeField] GameObject multicanva;




    private void Start()
    {
        multicanva.SetActive(false);
        SecendCamera.gameObject.SetActive(false);
    }


    public void MoveMultiSpace()
    {
        multicanva.SetActive(true);
        if (MainCamera != null) MainCamera.gameObject.SetActive(false);
        if(SecendCamera != null) SecendCamera.gameObject.SetActive(true);
    }

    public void MoveStartSpace()
    {
        multicanva.SetActive(false);
        if (MainCamera != null) MainCamera.gameObject.SetActive(true);
        if (SecendCamera != null) SecendCamera.gameObject.SetActive(false);
    }




}
