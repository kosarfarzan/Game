using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDice : MonoBehaviour
{
    // [SerializeField] attribute is used to make the private variables accessible in unity
    [SerializeField] Sprite[] numberSprite;
    [SerializeField] SpriteRenderer numberSpriteHolder;
    [SerializeField] SpriteRenderer rollingDiceAnimation;
    [SerializeField] int numberGot;

    public bool canDiceRoll = true;

    Coroutine generateRandomNumberDice;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnMouseDown()
    {
        generateRandomNumberDice = StartCoroutine(RollDice());
    }

    IEnumerator RollDice()
    {
        // Yield use like retun and its good for long lists
        yield return new WaitForEndOfFrame();

        // when dice is rolling, disable the mousedown to prevent twice roll
        if (canDiceRoll)
        {
            canDiceRoll = false;
            numberSpriteHolder.gameObject.SetActive(false);
            rollingDiceAnimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            // We create a random number and base on it, the sprite dice (1 to 6) will be shown
            numberGot = Random.Range(0, 6);
            numberSpriteHolder.sprite = numberSprite[numberGot];
            numberGot++;
            GameManager.gameManager.numberOfStepsToMove = numberGot;

            // Add this to find which dice is rolling and we define a condition for players
            GameManager.gameManager.rollingDice = this;

            numberSpriteHolder.gameObject.SetActive(true);
            rollingDiceAnimation.gameObject.SetActive(false);

            canDiceRoll = true;
            if (generateRandomNumberDice != null)
            {
                StopCoroutine(RollDice());
            }
        }


    }
}
