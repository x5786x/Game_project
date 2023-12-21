#define _CRT_SECURE_NO_WARNINGS
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
void firstRound(Player*); // �Ĥ@�^�X�o�P
void printBasicInfo(User);

int main()
{
	User user;
	Player player, banker;
	Card deck[NSUFTS * NRANKS];
	char ans, flag;
	int n;
	srand((unsigned)time(NULL));
	while (1)
	{
		flag = 0;

		// �n�J&���U���q
		while (flag != 1)
		{
			printf("�аݭn�n�J�٬O���U(1���n�J�B2�����U�B0���h�X): ");
			scanf(" %c", &ans);
			// �P�_�ϥΪ̿�ܵ��U�B�n�J�B�h�X
			switch (ans)
			{
			case '1':
				printf("�п�J�ϥΪ̦W��: ");
				scanf(" %s", user.username);
				printf("�п�J�K�X: ");
				scanf(" %s", user.password);
				if (loginUser(&user, MAX_USERS))
					flag = 1;
				else
					flag = 0;
				break;
			case '2':
				regsiterUser(&user);
				flag = 1;
				break;
			case '0':
				printf("...�{������...\n");
				Sleep(1000);

			}
		}

		//��ܪ��a�T��
		printf("���a�W�� %s\n", user.username);
		printf("���a�Ѿl����: %d\n", user.money);

		printf("�O�_�}�l�C��? (1���}�l�B0���W�@�B): ");
		scanf(" %c", &ans);
		if (ans == '0')
		{
			system("cls");
			continue;
		}
			
		while (1)
		{
			// ��l�ƱƲաB���a�ά~�P
			initializeDeck(deck);
			shuffleDeck(deck);
			initializePlayer(&player); initializePlayer(&banker);
			n = 0;

			// �U�`���q
			playerBet(&player, user);



			


		}
	}
	
	
	

	
}

void printBasicInfo(User user)
{
	
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

void playerBet(Player* player, User user)
{
	while (1)
	{
		printf("�ФU�`(�ثe�������B��%d): ", user.money);
		scanf("%d", &(player->bet));
		if (player->bet > user.money)
			printf("��J���B���~,�Э��s��J\n");
		else
			break;
	}
}

void firstRound(Player* player, int *n)
{
	
}