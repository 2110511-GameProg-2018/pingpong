using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Player player;
	public Field field;
	public Ball ball;
	public Button skipDefendButton;
	public Button simpleAttackButton;
	
    private InnerPhase currentPhase;
    private PhaseModel pm;
    
    // Use this for initialization
    void Start () {
		pm = GameObject.FindObjectOfType<PhaseModel> ();
        if (pm == null)
        {
            Debug.LogError("Cannot find any PhaseModels in Scene. Please add a PhaseModel and try again.");
        }
        currentPhase = InnerPhase.INITIATE_GAME;
		skipDefendButton.gameObject.SetActive (false);
		simpleAttackButton.gameObject.SetActive (false);
    }
	
	// Update is called once per frame
	void Update () {

        InnerPhase nextPhase = InnerPhase.ERROR;
		ball.setSpeed(10);   // TODO link with real ball speed

        // Calculate next phase
        switch (currentPhase)
        {
            case InnerPhase.INITIATE_GAME:
                initializeGame();
                nextPhase = InnerPhase.STANDBY;
                break;
            case InnerPhase.STANDBY:
                standby();
			if (ball.getSpeed() >= 15)
                {
                    nextPhase = InnerPhase.LOSE;
                } else
                {
                    nextPhase = InnerPhase.DRAW;
                }
                break;
            case InnerPhase.DRAW:
                pm.currentPhase = Phase.DRAW;
                draw();
				if (!canDrawCard)
                {
                    nextPhase = InnerPhase.LOSE;
                } else
                {
                    nextPhase = InnerPhase.DRAWING;
                }
                break;
			case InnerPhase.DRAWING:
				drawing ();
				if (!player.IsDrawFinished())
				{
					nextPhase = InnerPhase.DRAWING;
				} else
				{
					nextPhase = InnerPhase.DEFEND_STANDBY;
				}
				break;
            case InnerPhase.DEFEND_STANDBY:
                pm.currentPhase = Phase.DEFEND;
                defendStandby();
                nextPhase = InnerPhase.DEFEND_CARD_SELECT;
                break;
            case InnerPhase.DEFEND_CARD_SELECT:
                defendCardSelect();
				if (selectedCard != null)
                {
					skipDefendButton.gameObject.SetActive (false);
					nextPhase = InnerPhase.DEFEND_CARD_SELECTING;
				} else if (skipDefend /* skip button is pressed */)
                {
					player.SetHandSelectableType (false, false, false, false);
					skipDefend = false;
					skipDefendButton.gameObject.SetActive (false);
					nextPhase = InnerPhase.SET_DEFEND_FIELD;
                } else
                {
                    nextPhase = InnerPhase.DEFEND_CARD_SELECT;
                }
                break;
			case InnerPhase.DEFEND_CARD_SELECTING:
				defendCardSelecting();
				if (player.IsSelectFinished())
				{
					player.SetHandSelectableType (false, false, false, false);
					nextPhase = InnerPhase.DEFEND_CARD_EXECUTION;
				} else
				{
					nextPhase = InnerPhase.DEFEND_CARD_SELECTING;
				}
				break;
            case InnerPhase.DEFEND_CARD_EXECUTION:
                defendCardExecution();
				nextPhase = InnerPhase.SET_DEFEND_FIELD;
                break;
			case InnerPhase.SET_DEFEND_FIELD:
				setDefendField ();
				nextPhase = InnerPhase.DEFEND_FIELD_DIRECTION;
				break;
            case InnerPhase.DEFEND_FIELD_DIRECTION:
                pm.currentPhase = Phase.GUESS;
                defendFieldDirection();
				if (defendDirection != Direction.NONE /* Direction is selected */ )
                {
					if (defendDirection == attackDirection || attackDirection == Direction.NONE /* guessed right */)
                    {
                        nextPhase = InnerPhase.CONDITION_STANDBY;
                    }
					else if (ball.getSpeed() < 10)
                    {
                        nextPhase = InnerPhase.ATTACK_STANDBY;
                    }
                    else
                    {
                        nextPhase = InnerPhase.LOSE;
                    }
					attackDirection = Direction.NONE;
					defendDirection = Direction.NONE;
					field.gameObject.SetActive(false);
                } else /* Direction is not selected */
                {  
                    nextPhase = InnerPhase.DEFEND_FIELD_DIRECTION;
                }
                break;
            case InnerPhase.CONDITION_STANDBY:
                pm.currentPhase = Phase.COUNTER;
                conditionStandby();
                nextPhase = InnerPhase.CONDITION_CARD_SELECT;
                break;
            case InnerPhase.CONDITION_CARD_SELECT:
                conditionCardSelect();
				if (selectedCard != null /* Card is selected */)
                {
					if (selectedCard.GetBaseCard().GetCardType() == CardType.Effect /* card is condition card */)
                    {
                        nextPhase = InnerPhase.CONDITION_CARD_SELECTING;
                    }
					else if (selectedCard.GetBaseCard().GetCardType() == CardType.Attack /* card is attack card */ )
                    {
                        pm.currentPhase = Phase.ATTACK;
						nextPhase = InnerPhase.ATTACK_CARD_SELECTING;
                    }
                    else
                    {
                        Debug.LogError("Invalid Card selected ");
                        nextPhase = InnerPhase.ERROR;
                    }
					simpleAttackButton.gameObject.SetActive (false);
                }
				else if (ngoAttack /* Simple Attack button pressed */)
                {
					player.SetHandSelectableType (false, false, false, false);
					simpleAttackButton.gameObject.SetActive (false);
                    nextPhase = InnerPhase.SIMPLE_ATTACK;
                }
                else
                {
                    nextPhase = InnerPhase.CONDITION_CARD_SELECT;
                }
                break;
			case InnerPhase.CONDITION_CARD_SELECTING:
				conditionCardSelecting();
				if (player.IsSelectFinished())
				{
					player.SetHandSelectableType (false, false, false, false);
					nextPhase = InnerPhase.CONDITION_CARD_EXECUTION;
				} else
				{
					nextPhase = InnerPhase.CONDITION_CARD_SELECTING;
				}
				break;
            case InnerPhase.CONDITION_CARD_EXECUTION:
                conditionCardExecution();
                nextPhase = InnerPhase.ATTACK_STANDBY;
                break;
            case InnerPhase.ATTACK_STANDBY:
                pm.currentPhase = Phase.ATTACK;
                attackStandby();
                nextPhase = InnerPhase.ATTACK_CARD_SELECT;
                break;
            case InnerPhase.ATTACK_CARD_SELECT:
                attackCardSelect();
				if (selectedCard != null /* a card is selected -- not null */)
                {
					if (selectedCard.GetBaseCard().GetCardType() == CardType.Attack /* card is of attack type */)
                    {
						nextPhase = InnerPhase.ATTACK_CARD_SELECTING;
                    } else
                    {
                        Debug.LogError("Invalid card selected (not attack type)");
                        nextPhase = InnerPhase.ERROR;
                    }
					simpleAttackButton.gameObject.SetActive (false);
				} else if (ngoAttack /* simple attack button is pressed */ )
                {
					player.SetHandSelectableType (false, false, false, false);
					ngoAttack = false;
					simpleAttackButton.gameObject.SetActive (false);
                    nextPhase = InnerPhase.SIMPLE_ATTACK;
                } else
                {
                    nextPhase = InnerPhase.ATTACK_CARD_SELECT;
                }
                break;
			case InnerPhase.ATTACK_CARD_SELECTING:
				attackCardSelecting();
				if (player.IsSelectFinished())
				{
					player.SetHandSelectableType (false, false, false, false);
					nextPhase = InnerPhase.ATTACK_CARD_EXECUTION;
				} else
				{
					nextPhase = InnerPhase.ATTACK_CARD_SELECTING;
				}
				break;
            case InnerPhase.ATTACK_CARD_EXECUTION:
                attackCardExecution();
				nextPhase = InnerPhase.SET_ATTACK_FIELD;
                break;
			case InnerPhase.SET_ATTACK_FIELD:
				setAttackField ();
				nextPhase = InnerPhase.ATTACK_FIELD_DIRECTION;
				break;
			case InnerPhase.ATTACK_FIELD_DIRECTION:
				attackFieldDirection ();
				if (attackDirection != Direction.NONE /* Direction is selected */ )
				{
					nextPhase = InnerPhase.END;
					field.gameObject.SetActive(false);
				} else /* Direction is not selected */
				{  
					nextPhase = InnerPhase.ATTACK_FIELD_DIRECTION;
				}
                break;
            case InnerPhase.SIMPLE_ATTACK:
                simpleAttack();
				nextPhase = InnerPhase.SET_ATTACK_FIELD;
                break;
            case InnerPhase.LOSE:
                lose();
                nextPhase = InnerPhase.LOSE;
                Debug.Log("Player Lost!");
                break;
            case InnerPhase.END:
                pm.currentPhase = Phase.END;
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
		player.Initialize ();
		field.gameObject.SetActive (false);
    }
    private void standby()
    {

    }

	bool canDrawCard = true;
    private void draw()
    {
		canDrawCard = player.Draw ();
    }

	private void drawing()
	{
		
	}

    private void defendStandby()
    {
		player.SetHandSelectableType (false, true, false, false);
		skipDefendButton.gameObject.SetActive (true);
    }
	BaseCardComponent selectedCard;
    private void defendCardSelect()
    {
		selectedCard = player.SelectCard ();
    }
	private void defendCardSelecting()
	{
		
	}
    private void defendCardExecution()
    {
		selectedCard.Use ();
		GameObject.Destroy (selectedCard.gameObject);
		selectedCard = null;
    }
	private void setDefendField()
	{
		field.setDirectionState (true, true, true, true);
		field.gameObject.SetActive (true);
	}
	Direction defendDirection = Direction.NONE;
    private void defendFieldDirection()
    {
		defendDirection = field.getInputDirection ();
    }

    private void conditionStandby()
    {
		simpleAttackButton.gameObject.SetActive (true);
		player.SetHandSelectableType (true, false, true, false);
    }

    private void conditionCardSelect()
    {
		selectedCard = player.SelectCard ();
    }
	private void conditionCardSelecting()
	{

	}
    private void conditionCardExecution()
    {
		selectedCard.Use ();
		GameObject.Destroy (selectedCard.gameObject);
		selectedCard = null;
    }
    private void attackStandby()
    {
		simpleAttackButton.gameObject.SetActive (true);
		player.SetHandSelectableType (true, false, false, false);
    }
    private void attackCardSelect()
    {
		selectedCard = player.SelectCard ();
    }
	private void attackCardSelecting()
	{
		
	}
    private void attackCardExecution()
    {
		selectedCard.Use ();
		GameObject.Destroy (selectedCard.gameObject);
		selectedCard = null;
    }
	private void setAttackField()
	{
		field.setDirectionState (true, true, true, true);
		field.gameObject.SetActive (true);
	}
	Direction attackDirection = Direction.NONE;
    private void attackFieldDirection()
    {
		attackDirection = field.getInputDirection ();
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

	bool skipDefend = false;
	public void SkipDefend() {
		skipDefend = true;
	}
	bool ngoAttack = false;
	public void SimpleAttack() {
		ngoAttack = true;
	}
}
