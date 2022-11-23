using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public static DragScript hurufSedangDrag;
    [SerializeField] TMPro.TextMeshProUGUI hurufDisplay;

    private bool petunjuk, terisi;
    private Vector3 posisiAwal;
    private Transform parentAwal;
    public string Huruf{get; private set;}

    public void Inisialisasi(Transform parent, string huruf, bool petunjuk){
        Huruf = huruf;
        transform.SetParent(parent);
        hurufDisplay.text = huruf;
        this.petunjuk = petunjuk;
        GetComponent<CanvasGroup>().alpha = petunjuk ? 0.5f : 1f;

    }

    public void Match(Transform parent){
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        petunjuk = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(petunjuk) 
            return;
        posisiAwal = transform.position;
        parentAwal = transform.parent;
        hurufSedangDrag = this;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(petunjuk) 
            return;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(petunjuk) 
            return;
        hurufSedangDrag = null;

        if(transform.parent == parentAwal){
            transform.position = posisiAwal;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    // method ini akan jalan pada huruf yang merupakan bagian dari petunjuk
    public void OnDrop(PointerEventData eventData)
    {
        if(petunjuk && !terisi){
            if(hurufSedangDrag.Huruf == Huruf){
                ManagerKata.instance.tambahPoint();
                hurufSedangDrag.Match(transform);
                terisi = true;
                GetComponent<CanvasGroup>().alpha = 1f;
            }
        }
    }


    public void OnClickFalse(){
        if(petunjuk)
            return;
        ManagerKata posAkhir = transform.parent.GetComponent<ManagerKata>();
        if(posAkhir != null){
            posAkhir = null;
            hurufSedangDrag.Huruf = null;
        }
        
    }
    public void OnClickTrue(){
        if(petunjuk)
            return;
        ManagerKata posAwal = transform.parent.GetComponent<ManagerKata>();
        if(posAwal != null){
            hurufSedangDrag.Huruf = Huruf;
        }
    }
}
