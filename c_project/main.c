#define _CRT_SECURE_WARINRINGS
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "user_authentication.h"

#define MAX_USERS 50
#define NSUFTS 4
#define NRANKS 13

const char* sufts[NSUFTS] = { "方塊", "梅花", "紅心", "黑桃" };
const char* ranks[NRANKS] = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};

typedef struct
{
	int suitIndex;
	int rankIndex;
	int value[2];
}Card;

typedef struct
{
	Card *hand;
	int score;
	int bet;
}Player;

void initializeDeck(Card*); // 初始化牌組
void shuffleDeck(Card*); // 洗牌
void initializePlayer(Player*); // 每回合重製玩家數值
void playerBet(Player*); // 玩家下注


int main()
{
	User user;
	Player player;
	Card deck[NSUFTS * NRANKS];
	char q;
	srand((unsigned)time(NULL));

	// 登入&註冊階段
	while (1)
	{
		printf("請問要登入還是註冊(1為登入，2為註冊): ");
		scanf(" %c", &q);
		switch (q)
		{
			case '1':
				printf("請輸入使用者名稱: ");
				scanf(user.username);
				printf("請輸入密碼: ");
				scanf(user.password);
				loginUser(user, MAX_USERS);
				break;
			case '2':
				regsiterUser(&user);
		}
	}

		
	initializeDeck(deck);
	shuffleDeck(deck);

	while (1)
	{

	}

	
}


void initializeDeck(Card *deck) 
{
	for (int i = 0; i < NSUFTS; i++)
	{
		for (int j = 0; j < NRANKS; j++)
		{
			deck[i * NRANKS + j].suitIndex = i;
			deck[i * NRANKS + j].rankIndex = j;
			if (j == 0)
			{
				deck[i * NRANKS + j].value[0] = 1;
				deck[i * NRANKS + j].value[1] = 11;
			}
			else if (j > 9)
			{
				deck[i * NRANKS + j].value[0] = 10;
				deck[i * NRANKS + j].value[1] = 10;
			}
			else
			{
				deck[i * NRANKS + j].value[0] = j + 1;
				deck[i * NRANKS + j].value[1] = j + 1;
			}
		}
	}
}

void shuffleDeck(Card *deck) 
{
	Card temp;
	int cards = NSUFTS * NRANKS, n;
	for (int i = 0; i < cards; i++)
	{
		n = rand() % (cards - i) + i;
		temp = deck[i];
		deck[i] = deck[n];
		deck[n] = temp;
	}
}

void initializePlayer(Player* player)
{
	player->hand = NULL;
	player->score = 0;
	player->bet = 0;
}

void playerBet(Player* player)
{
	printf("請下注: ");
	scanf("%d", player->bet);
}