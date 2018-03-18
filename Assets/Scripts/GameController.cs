using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private enum InnerPhase
    {
        INITIATE_GAME, STANDBY, DRAW,
        DEFEND_STANDBY, DEFEND_CARD_SELECT, DEFEND_CARD_EXECUTION, DEFEND_FIELD_DIRECTION,
        CONDITION_STANDBY, CONDITION_CARD_SELECT, CONDITION_CARD_EXECUTION,
        ATTACK_STANDBY, ATTACK_CARD_SELECT, ATTACK_CARD_EXECUTION, ATTACK_FIELD_DIRECTION,
        SIMPLE_ATTACK, LOSE, END,
        ERROR
    }

    InnerPhase currentPhase;
    PhaseModel pm = GameObject.FindObjectOfType<PhaseModel>();
    
    // Use this for initialization
    void Start () {
        currentPhase = InnerPhase.INITIATE_GAME;
    }
	
	// Update is called once per frame
	void Update () {

        InnerPhase nextPhase = InnerPhase.ERROR;
        float ballSpeed = 10;   // TODO link with real ball speed

        // Calculate next phase
        switch (currentPhase)
        {
            case InnerPhase.INITIATE_GAME:
                initializeGame();
                nextPhase = InnerPhase.STANDBY;
                break;
            case InnerPhase.STANDBY:
                standby();
                if (ballSpeed >= 15)
                {
                    nextPhase = InnerPhase.LOSE;
                } else
                {
                    nextPhase = InnerPhase.DRAW;
                }
                break;
            case InnerPhase.DRAW:
                draw();
                if (false /* cannot draw a card, deck is empty */)
                {
                    nextPhase = InnerPhase.LOSE;
                } else
                {
                    nextPhase = InnerPhase.DEFEND_STANDBY;
                }
                break;
            case InnerPhase.DEFEND_STANDBY:
                defendStandby();
                nextPhase = InnerPhase.DEFEND_CARD_SELECT;
                break;
            case InnerPhase.DEFEND_CARD_SELECT:
                defendCardSelect();
                if (null != null /* A card is selected */)
                {
                    nextPhase = InnerPhase.DEFEND_CARD_EXECUTION;
                } else if (true /* skip button is pressed */)
                {
                    nextPhase = InnerPhase.DEFEND_FIELD_DIRECTION;
                } else
                {
                    nextPhase = InnerPhase.DEFEND_CARD_SELECT;
                }
                break;
            case InnerPhase.DEFEND_CARD_EXECUTION:
                defendCardExecution();
                nextPhase = InnerPhase.DEFEND_FIELD_DIRECTION;
                break;
            case InnerPhase.DEFEND_FIELD_DIRECTION:
                defendFieldDirection();
                if (null != null /* Direction is selected */ )
                {
                    if (true /* guessed right */)
                    {
                        nextPhase = InnerPhase.CONDITION_STANDBY;
                    }
                    else if (ballSpeed < 10)
                    {
                        nextPhase = InnerPhase.ATTACK_STANDBY;
                    }
                    else
                    {
                        nextPhase = InnerPhase.LOSE;
                    }
                } else /* Direction is not selected */
                {  
                    nextPhase = InnerPhase.DEFEND_FIELD_DIRECTION;
                }
                break;
            case InnerPhase.CONDITION_STANDBY:
                conditionStandby();
                nextPhase = InnerPhase.CONDITION_CARD_SELECT;
                break;
            case InnerPhase.CONDITION_CARD_SELECT:
                conditionCardSelect();
                if (null != null /* Card is selected */)
                {
                    if (true /* card is condition card */)
                    {
                        nextPhase = InnerPhase.CONDITION_CARD_EXECUTION;
                    }
                    else if (true /* card is attack card */ )
                    {
                        nextPhase = InnerPhase.ATTACK_CARD_EXECUTION;
                    }
                    else
                    {
                        Debug.LogError("Invalid Card selected ");
                        nextPhase = InnerPhase.ERROR;
                    }
                }
                else if (true /* Simple Attack button pressed */)
                {
                    nextPhase = InnerPhase.SIMPLE_ATTACK;
                }
                else
                {
                    nextPhase = InnerPhase.CONDITION_CARD_SELECT;
                }
                break;
            case InnerPhase.CONDITION_CARD_EXECUTION:
                conditionCardExecution();
                nextPhase = InnerPhase.ATTACK_STANDBY;
                break;
            case InnerPhase.ATTACK_STANDBY:
                attackStandby();
                nextPhase = InnerPhase.ATTACK_CARD_SELECT;
                break;
            case InnerPhase.ATTACK_CARD_SELECT:
                attackCardSelect();
                if (null != null /* a card is selected -- not null */)
                {
                    if (true /* card is of attack type */)
                    {
                        nextPhase = InnerPhase.ATTACK_CARD_EXECUTION;
                    } else
                    {
                        Debug.LogError("Invalid card selected (not attack type)");
                        nextPhase = InnerPhase.ERROR;
                    }
                } else if (true /* simple attack button is pressed */ )
                {
                    nextPhase = InnerPhase.SIMPLE_ATTACK;
                } else
                {
                    nextPhase = InnerPhase.ATTACK_CARD_SELECT;
                }
                break;
            case InnerPhase.ATTACK_CARD_EXECUTION:
                attackCardExecution();
                nextPhase = InnerPhase.ATTACK_FIELD_DIRECTION;
                break;
            case InnerPhase.ATTACK_FIELD_DIRECTION:
                attackFieldDirection();
                nextPhase = InnerPhase.END;
                break;
            case InnerPhase.SIMPLE_ATTACK:
                simpleAttack();
                nextPhase = InnerPhase.ATTACK_FIELD_DIRECTION;
                break;
            case InnerPhase.LOSE:
                lose();
                nextPhase = InnerPhase.LOSE;
                Debug.Log("Player Lost!");
                break;
            case InnerPhase.END:
                end();
                nextPhase = InnerPhase.STANDBY;
                break;
            case InnerPhase.ERROR:
                Debug.Log("Error occurred in game controller");
                break;
            default:
                nextPhase = InnerPhase.ERROR;
                break;
        }
        currentPhase = nextPhase;
    }

    private void initializeGame()
    {

    }
    private void standby()
    {

    }

    private void draw()
    {

    }

    private void defendStandby()
    {

    }

    private void defendCardSelect()
    {

    }

    private void defendCardExecution()
    {

    }
    private void defendFieldDirection()
    {

    }
    private void conditionStandby()
    {

    }

    private void conditionCardSelect()
    {

    }
    private void conditionCardExecution()
    {

    }
    private void attackStandby()
    {

    }
    private void attackCardSelect()
    {

    }
    private void attackCardExecution()
    {

    }
    private void attackFieldDirection()
    {

    }
    private void simpleAttack()
    {

    }
    private void lose()
    {

    }
    private void end()
    {

    }
}
