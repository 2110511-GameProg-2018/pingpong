using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Player p1;
	public Player p2;
	public Field field;
	public Ball ball;
	public Button skipDefendButton;
	public Button simpleAttackButton;
    public BallIndicator ballIndicator;
	
	private Player currentPlayer;
    private InnerPhase currentPhase;
    private PhaseModel pm;

    public Text winText;
    private int turn;
    
    // Use this for initialization
    void Start () {
        winText.text = "";
        turn = 1;
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
				if (!currentPlayer.IsDrawFinished())
				{
					nextPhase = InnerPhase.DRAWING;
				} else
				{
					if (turn > 1) /* Not the first turn */
                    {
                        nextPhase = InnerPhase.DEFEND_STANDBY;
                    } else /* If first turn, just skip the guessing and jump to the attack*/ 
                    {
                        nextPhase = InnerPhase.ATTACK_STANDBY;
                    }
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
					skipDefendButton.gameObject.SetActive (false);
					skipDefend = false;
					currentPlayer.SetHandSelectableType (false, false, false, false);
					nextPhase = InnerPhase.SET_DEFEND_FIELD;
                } else
                {
                    nextPhase = InnerPhase.DEFEND_CARD_SELECT;
                }
                break;
			case InnerPhase.DEFEND_CARD_SELECTING:
				defendCardSelecting();
				if (currentPlayer.IsSelectFinished())
				{
					currentPlayer.SetHandSelectableType (false, false, false, false);
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
                    ballIndicator.ShowIndicator(attackDirection);
                    nextPhase = InnerPhase.DEFEND_SHOW_BALL_INDICATOR;
                } else /* Direction is not selected */
                {  
                    nextPhase = InnerPhase.DEFEND_FIELD_DIRECTION;
                }
                break;
            case InnerPhase.DEFEND_SHOW_BALL_INDICATOR:
                if (Input.GetMouseButtonDown(0) /* Go next on mouse click */)
                {
                    ballIndicator.HideIndicator();
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
                }
                else // User has not clicked then stay in same phase
                {
                    nextPhase = InnerPhase.DEFEND_SHOW_BALL_INDICATOR;
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
					currentPlayer.SetHandSelectableType (false, false, false, false);
					ngoAttack = false;
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
				if (currentPlayer.IsSelectFinished())
				{
					currentPlayer.SetHandSelectableType (false, false, false, false);
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
					currentPlayer.SetHandSelectableType (false, false, false, false);
					simpleAttackButton.gameObject.SetActive (false);
					ngoAttack = false;
                    nextPhase = InnerPhase.SIMPLE_ATTACK;
                } else
                {
                    nextPhase = InnerPhase.ATTACK_CARD_SELECT;
                }
                break;
			case InnerPhase.ATTACK_CARD_SELECTING:
				attackCardSelecting();
				if (currentPlayer.IsSelectFinished())
				{
					currentPlayer.SetHandSelectableType (false, false, false, false);
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
		p1.Initialize ();
		p2.Initialize ();
		field.gameObject.SetActive (false);
		ball.setSpeed(0);
		currentPlayer = p1;
		p2.gameObject.SetActive (false);
    }
    private void standby()
    {

    }

	bool canDrawCard = true;
    private void draw()
    {
		canDrawCard = currentPlayer.Draw ();
    }

	private void drawing()
	{
		
	}

    private void defendStandby()
    {
		skipDefendButton.gameObject.SetActive (true);
		currentPlayer.SetHandSelectableType (false, true, false, false);
    }
	BaseCardComponent selectedCard;
    private void defendCardSelect()
    {
		selectedCard = currentPlayer.SelectCard ();
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
		currentPlayer.SetHandSelectableType (true, false, true, false);
    }

    private void conditionCardSelect()
    {
		selectedCard = currentPlayer.SelectCard ();
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
		currentPlayer.SetHandSelectableType (true, false, false, false);
    }
    private void attackCardSelect()
    {
		selectedCard = currentPlayer.SelectCard ();
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
		ball.setSpeed (ball.getSpeed() + 1);
    }
    private void lose()
    {
        if(turn%2 == 1)
            winText.text = "Player2 Win!!";
        else
            winText.text = "Player1 Win!!";
    }
    private void end()
    {
		if (currentPlayer == p1) {
			currentPlayer = p2;
			p2.gameObject.SetActive (true);
			p1.gameObject.SetActive (false);
		}
		else {
			currentPlayer = p1;
			p1.gameObject.SetActive (true);
			p2.gameObject.SetActive (false);
		}

        // Increment the turn count
        turn++;
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
