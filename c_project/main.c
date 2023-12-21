#define _CRT_SECURE_WARINRINGS
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "user_authentication.h"

#define MAX_USERS 50
#define NSUFTS 4
#define NRANKS 13

const char* sufts[NSUFTS] = { "���", "����", "����", "�®�" };
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

void initializeDeck(Card*); // ��l�ƵP��
void shuffleDeck(Card*); // �~�P
void initializePlayer(Player*); // �C�^�X���s���a�ƭ�
void playerBet(Player*); // ���a�U�`


int main()
{
	User user;
	Player player;
	Card deck[NSUFTS * NRANKS];
	char q;
	srand((unsigned)time(NULL));

	// �n�J&���U���q
	while (1)
	{
		printf("�аݭn�n�J�٬O���U(1���n�J�A2�����U): ");
		scanf(" %c", &q);
		switch (q)
		{
			case '1':
				printf("�п�J�ϥΪ̦W��: ");
				scanf(user.username);
				printf("�п�J�K�X: ");
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
	printf("�ФU�`: ");
	scanf("%d", player->bet);
}