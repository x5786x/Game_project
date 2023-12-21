#define _CRT_SECURE_NO_WARNINGS
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
void firstRound(Player*); // 第一回合發牌
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

		// 登入&註冊階段
		while (flag != 1)
		{
			printf("請問要登入還是註冊(1為登入、2為註冊、0為退出): ");
			scanf(" %c", &ans);
			// 判斷使用者選擇註冊、登入、退出
			switch (ans)
			{
			case '1':
				printf("請輸入使用者名稱: ");
				scanf(" %s", user.username);
				printf("請輸入密碼: ");
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
				printf("...程式結束...\n");
				Sleep(1000);

			}
		}

		//顯示玩家訊息
		printf("玩家名稱 %s\n", user.username);
		printf("玩家剩餘金錢: %d\n", user.money);

		printf("是否開始遊戲? (1為開始、0為上一步): ");
		scanf(" %c", &ans);
		if (ans == '0')
		{
			system("cls");
			continue;
		}
			
		while (1)
		{
			// 初始化排組、玩家及洗牌
			initializeDeck(deck);
			shuffleDeck(deck);
			initializePlayer(&player); initializePlayer(&banker);
			n = 0;

			// 下注階段
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
		printf("請下注(目前持有金額為%d): ", user.money);
		scanf("%d", &(player->bet));
		if (player->bet > user.money)
			printf("輸入金額錯誤,請重新輸入\n");
		else
			break;
	}
}

void firstRound(Player* player, int *n)
{
	
}