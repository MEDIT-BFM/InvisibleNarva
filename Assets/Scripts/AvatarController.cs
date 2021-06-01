using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour {
    public SceneShifter sceneChange;
    public Image avatarButtonPanel;
    public Image AvatarSelectionPanel;
    public Image FirstAvatarPanel;
    public Image SecondAvatarPanel;
    public Image ThirdAvatarPanel;
    public Image ForthAvatarPanel;
    public Toggle[] avatarToggle;
    public Image[] DoneMark;

    public static bool[] Avatar_isCompleted = new bool[4];

    private void Update() {
        CheckToggleSelection();
        for (int i = 0; i < DoneMark.Length; i++) {
            if (Avatar_isCompleted[i]) {
                DoneMark[i].gameObject.SetActive(true);
            }
            else {
                DoneMark[i].gameObject.SetActive(false);
            }
        }
    }

    private void CheckToggleSelection() {
        if (!avatarToggle[0].isOn && !avatarToggle[1].isOn && !avatarToggle[2].isOn && !avatarToggle[3].isOn) {
            avatarButtonPanel.gameObject.SetActive(false);
        }
        else {
            avatarButtonPanel.gameObject.SetActive(true);
        }
    }

    public void AvatarSceneTransition() {
        StartCoroutine(SelectedAvatar());
    }

    public void AvatarSceneTransitionReturn(int index) {
        StartCoroutine(SelectedAvatarReturn(index));
    }

    IEnumerator SelectedAvatar() {

        yield return new WaitForSeconds(.5f);
        AvatarSelectionPanel.gameObject.SetActive(false);
        if (avatarToggle[0].isOn) {
            FirstAvatarPanel.gameObject.SetActive(true);
            FirstAvatarPanel.GetComponent<Animator>().SetBool("IsBacked", false);
        }
        if (avatarToggle[1].isOn) {
            SecondAvatarPanel.gameObject.SetActive(true);
            SecondAvatarPanel.GetComponent<Animator>().SetBool("IsBacked", false);
        }
        if (avatarToggle[2].isOn) {
            ThirdAvatarPanel.gameObject.SetActive(true);
            ThirdAvatarPanel.GetComponent<Animator>().SetBool("IsBacked", false);
        }
        if (avatarToggle[3].isOn) {
            ForthAvatarPanel.gameObject.SetActive(true);
            ForthAvatarPanel.GetComponent<Animator>().SetBool("IsBacked", false);
        }
    }

    IEnumerator SelectedAvatarReturn(int index) {
        yield return new WaitForSeconds(.5f);
        switch (index) {
            case 1:
                FirstAvatarPanel.gameObject.SetActive(false);
                break;
            case 2:
                SecondAvatarPanel.gameObject.SetActive(false);
                break;
            case 3:
                ThirdAvatarPanel.gameObject.SetActive(false);
                break;
            case 4:
                ForthAvatarPanel.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        AvatarSelectionPanel.gameObject.SetActive(true);
        AvatarSelectionPanel.GetComponent<Animator>().SetBool("IsForwarded", false);
    }

    public void SceneChange(string name) {
        sceneChange.sceneName = name;
        sceneChange.FadeTransition();
    }
}
