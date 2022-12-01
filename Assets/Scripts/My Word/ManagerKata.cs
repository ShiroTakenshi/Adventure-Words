// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;
// using UnityEngine;

// public class ManagerKata : MonoBehaviour
// {
//     public static ManagerKata instance{get; private set;}
//     [SerializeField] DragScript hurufPrefab;
//     [SerializeField] Transform slotAwal, slotAkhir;
//     [SerializeField] string[] listkata;

//     private int poinKata, poin;
//     // Start is called before the first frame update
//     void Start()
//     {
//         instance = this;
//         InitKata(listkata[Random.Range(0, listkata.Length)]);
//     }

//     void InitKata(string kata){
//         char[] hurufKata = kata.ToCharArray();
//         char[] hurufAcak = new char[hurufKata.Length];

//         List<char> hurufKataCopy = new List<char>();
//         hurufKataCopy = hurufKata.ToList();

//         for(int i = 0; i < hurufKata.Length; i++){
//             int randomIndex = Random.Range(0, hurufKataCopy.Count);
//             hurufAcak[i] = hurufKataCopy[randomIndex];
//             hurufKataCopy.RemoveAt(randomIndex);

//             DragScript temp = Instantiate(hurufPrefab, slotAwal);

//             temp.Inisialisasi(slotAwal, hurufAcak[i].ToString(), false);
//         }

//         for (int i = 0; i < hurufKata.Length; i++)
//         {
//             DragScript temp = Instantiate(hurufPrefab, slotAkhir);
//             temp.Inisialisasi(slotAkhir, hurufKata[i].ToString(), true);
//         }

//         poinKata = hurufKata.Length;
//     }
//     public void tambahPoint(){
//         poin++;
//         if(poin == poinKata){
//             Debug.Log("Kata Benar");
//         }
//     }

// }
