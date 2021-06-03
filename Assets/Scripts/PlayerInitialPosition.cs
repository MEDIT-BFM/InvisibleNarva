using UnityEngine;

public class PlayerInitialPosition : MonoBehaviour {

    public bool startLocation = false;
    private GameObject Player;

    private void Awake() {
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
    }

    void Start() {
        if (startLocation) {
            //Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = false;
            Player.transform.position = gameObject.transform.position;
            Player.transform.rotation = gameObject.transform.rotation;
            //Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
        }
    }
}