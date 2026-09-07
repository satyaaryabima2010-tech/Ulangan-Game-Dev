using System;
using UnityEngine;

// PELAJARAN 1 — Delegate
// Tempel script ini ke Empty GameObject, Play, buka Console.
// Delegate = "kotak yang bisa menyimpan method" (bukan angka, bukan teks).
public class BelajarDelegate : MonoBehaviour
{
    // Kita buat tipe baru: method yang tidak mengembalikan nilai, dan tidak butuh parameter.
    delegate void AksiSederhana();

    void Start()
    {
        BelajarDelegate1();
        BelajarDelegate2();
        BelajarDelegate3();
    }

    void BelajarDelegate1()
    {
        AksiSederhana kotak = TulisHalo;
        kotak();
    }

    void BelajarDelegate2()
    {
        AksiSederhana kotak = TulisHalo;
        kotak += TulisDunia;
        kotak();
    }

    void BelajarDelegate3()
    {
        // Action sudah disediakan C#. Sama seperti delegate void ...() di atas.
        Action kotak = TulisHalo;
        kotak += TulisDunia;
        kotak();
    }

    void TulisHalo()
    {
        Debug.Log("Halo asu");

    }

    void TulisDunia()
    {
        Debug.Log("Dunia");
    }
}