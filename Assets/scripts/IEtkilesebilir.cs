using UnityEngine;

public interface IEtkilesilebilir
{
    Transform EtkilesimNoktasi { get; }
    bool Kullanilabilir { get; }    
    void EtkilesimeGir();
}