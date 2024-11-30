using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokingGalss : MonoBehaviour
{
    [SerializeField] private Transform _brokeObject;
    public float magnitudeCol, radius, power, upward;

    private GameObject Player;

    private void Awake()
    {
        Player = GameObject.Find("PlayerCharacter(AudioInput)");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= magnitudeCol)
        {
            AudioManager.Instance.PlaySound3D("BrokeGlass", Player.transform, 0, false, SoundType.SFX, true, 0, 5);
            gameObject.SetActive(false);
            Instantiate(_brokeObject, transform.position,transform.rotation);
            _brokeObject.localScale = transform.localScale;
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            foreach(Collider hit in colliders)
            {
                if(hit.transform.TryGetComponent(out Rigidbody rigid))
                {
                    rigid.AddExplosionForce(power * collision.relativeVelocity.magnitude, explosionPos, radius, upward);
                }
            }
        }
    }
}
