using UnityEngine;

public class BeklemeDurumu : IDurum
{
    public IDurum donulecekDurum;   
    public float beklemeSuresi = 0f;
    public float enFazlaBekleme = 30f;

    public BeklemeDurumu(IDurum donulecek)
    {
        donulecekDurum = donulecek;
    }

    public void Giris(YardimciKarakter karakter)
    {
        karakter.ajan.isStopped = true;   
        beklemeSuresi = 0f;
    }

    public void Guncelle(YardimciKarakter karakter)
    {
        beklemeSuresi = beklemeSuresi + Time.deltaTime;

        if (karakter.TehlikeVarMi() == false || beklemeSuresi > enFazlaBekleme)
        {
            karakter.DurumDegistir(donulecekDurum);
        }
    }

    public void Cikis(YardimciKarakter karakter)
    {
        karakter.ajan.isStopped = false;
    }
}