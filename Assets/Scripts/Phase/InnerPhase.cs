﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InnerPhase {
	INITIATE_GAME			= 0,
	STANDBY					= 1,
	DRAW					= 2,
	DRAWING					= 3,
	DEFEND_STANDBY			= 4,
	DEFEND_CARD_SELECT		= 5,
	DEFEND_CARD_SELECTING	= 101,
	DEFEND_CARD_EXECUTION	= 6,
	SET_DEFEND_FIELD		= 102,
	DEFEND_FIELD_DIRECTION	= 7,
	CONDITION_STANDBY		= 8,
	CONDITION_CARD_SELECT	= 9,
	CONDITION_CARD_SELECTING= 103,
	CONDITION_CARD_EXECUTION= 10,
	ATTACK_STANDBY			= 11,
	ATTACK_CARD_SELECT		= 12,
	ATTACK_CARD_SELECTING	= 104,
	ATTACK_CARD_EXECUTION	= 13,
	SET_ATTACK_FIELD		= 105,
	ATTACK_FIELD_DIRECTION	= 14,
	SIMPLE_ATTACK			= 15,
	LOSE					= 16,
	END						= 17,
	ERROR					= 18
}
