using UnityEngine;

public class DestroyWithEffect : MonoBehaviour
{
    public GameObject particleEffect; // Partikül prefab'ını buraya atayacaksın.
    public float destroyDelay = 1f;   // Efekt oynadıktan sonra prefab'ın yok olma süresi.

    void DestroyObject()
    {
        // Partikül efektini oluştur.
        GameObject effect = Instantiate(particleEffect, transform.position, Quaternion.identity);

        // Efektin yok edilmesini sağla.
        Destroy(effect, 2f); // Efektin ne kadar süre sahnede kalacağını belirle (2 saniye gibi).

        // Prefab'ı yok et.
        Destroy(gameObject, destroyDelay);
    }
}
