using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCursor : MonoBehaviour {
    private Image _image;

    private AbilityWrapper ability;
    public AbilityWrapper Ability {
        get { return ability; }
        set {
            ability = value;
            if (value != null) {
                _image.sprite = ability.ActiveAbility.aSprite;
                _image.enabled = true;
                transform.position = Input.mousePosition;
                gameObject.SetActive(true);
            } else {
                _image.sprite = null;
                _image.enabled = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void Awake() {
        _image = GetComponent<Image>();
        _image.sprite = null;
        _image.enabled = false;

    }

    private void Start() {
        gameObject.SetActive(false);
    }

    private void Update() {
        transform.position = Input.mousePosition;
    }

}