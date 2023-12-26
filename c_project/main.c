#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "user_authentication.h"

#define MAX_USERS 50
#define NSUFTS 4
#define NRANKS 13
#define MAX_HAND_CARDS 5


int currentCardIndex = 0;
const char* sufts[NSUFTS] = { "���", "����", "����", "�®�" };
const char* ranks[NRANKS] = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};

typedef struct
{
	int suitIndex;
	int rankIndex;
	int value;
}Card;

typedef struct
{
	Card hand[5];
	int numCard;
	int point;
	int bet;
}Player;

void initializeDeck(Card*); // ��l�ƵP��
void shuffleDeck(Card*); // �~�P
void initializePlayer(Player*); // �C�^�X���s���a�ƭ�
void playerBet(Player*); // ���a�U�`
void deal(Player*, Card*); // ��P
int checkHandCardsNumber(Player); // �ˬd��P�O�_�W�L5�i
void printBasicInfo(Player, int); // �L�X�ثe���a�T��
int checkPoint(Player); // �ˬd�O�_�z�P �^��0(�z�P)�B1(�C��21)�B2(����21);

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

		// ��ܪ��a�T��
		printf("���a�W�� %s\n", user.username);
		printf("���a�Ѿl����: %d\n", user.money);

		printf("�O�_�}�l�C��? (1���}�l�B0���W�@�B): ");
		scanf(" %c", &ans);
		if (ans == '0')
			continue;
		system("cls");

		// �C���}�l
		while (1)
		{
			// ��l�ƱƲաB���a�ά~�P
			initializeDeck(deck);
			shuffleDeck(deck);
			initializePlayer(&player); initializePlayer(&banker);
			n = 0;

			// �U�`���q
			playerBet(&player, user);

			// �Ĥ@�^�X�o�P
			for (int i = 0; i < 2; i++)
			{
				deal(&player, &deck);
				deal(&banker, &deck);
			}

			// �L�X����ثe�������P
			printf("�z�ثe���P��: "); printBasicInfo(player, 1);
			printf("���a�ثe���P��: "); printBasicInfo(banker, 0);
			
			

			// ���a�^�X
			while (checkHandCardsNumber(player))
			{
				
				char chice;
				// �C���ˬd�O�_�z�P
				if (checkPoint(player) == 0)
					break;
				printf("\n���P(1)���P(0): ");
				scanf(" %c", &chice);
				// �P�_�O�_���T��J
				if (chice != '1' && chice != '0')
				{
					printf("��J���~,�Э��s��J\n");
					continue;
				}
				else
				{
					// �p�G��1�h���P,�æL�X�ثe�����P
					if (chice == '1')
					{
						deal(&player, &deck);
						printf("�z�ثe���P��: "); printBasicInfo(player);
					}
					// �p�G��0�h���a�^�X����
					else
						break;
				}
				
			}

		}
	}
}

void deal(Player* player, Card *deck)
{
	player->hand[player->numCard] = *(deck + currentCardIndex);
	player->numCard++;
	currentCardIndex++;
}

int checkHandCardsNumber(Player player)
{
	if (player.numCard < 5)
		return 1;
	else
		return 0;
}

void printBasicInfo(Player player, int isPlayer)
{
	int i = 0;
	// ���a�Ĥ@�i�P���t�P�����
	if (isPlayer != 1)
	{
		i = 1;
		printf("? ");
	}
	
	for (;i < player.numCard; i++)
		printf("%s%s ", sufts[player.hand->suitIndex], ranks[player.hand->rankIndex]);
	printf('\n');
	isPlayer ? printf("�ثe��%d�i�P\n", player.numCard) : NULL;
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
				deck[i * NRANKS + j].value = 11;
			else if (j > 9)
				deck[i * NRANKS + j].value = 10;
			else
				deck[i * NRANKS + j].value = j + 1;
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
	player->numCard = 0;
	player->point = 0;
	player->bet = 0;
}

void playerBet(Player* player, User user)
{
	while (1)
	{
		printf("�ФU�`(�ثe�������B��%d): ", user.money);
		scanf("%d", &(player->bet));
		// ��J���B���ŦX�h���s��J
		if (player->bet > user.money || player->bet <= 0)
			printf("��J���B���~,�Э��s��J\n");
		else
			break;
	}
}

int checkPoint(Player player)
{
	int count = 0;
	player.point = 0;
	// �p��ثe�I��
	for (int i = 0; i < player.numCard; i++)
	{
		if (player.hand->rankIndex == 0)
			count++;
		player.point += player.hand->value;
	}
	while(count)
	{
		// �YA��11���I�ƶW�L21,�hA��1
		if (player.point > 21)
		{
			player.point -= 10;
			count--;
		}
		// �Y�I�ƨS�W�L21�h�h�X�ˬd
		else
			break;
	}

	// �ˬd�O�_�z�P
	if (player.point > 21)
		return 0;
	else if (player.point < 21)
		return 1;
	else
		return 2;
}
