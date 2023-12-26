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
const char* sufts[NSUFTS] = { "方塊", "梅花", "紅心", "黑桃" };
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

void initializeDeck(Card*); // 初始化牌組
void shuffleDeck(Card*); // 洗牌
void initializePlayer(Player*); // 每回合重製玩家數值
void playerBet(Player*); // 玩家下注
void deal(Player*, Card*); // 抽牌
int checkHandCardsNumber(Player); // 檢查手牌是否超過5張
void printBasicInfo(Player, int); // 印出目前玩家訊息
int checkPoint(Player); // 檢查是否爆牌 回傳0(爆牌)、1(低於21)、2(等於21);

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

		// 顯示玩家訊息
		printf("玩家名稱 %s\n", user.username);
		printf("玩家剩餘金錢: %d\n", user.money);

		printf("是否開始遊戲? (1為開始、0為上一步): ");
		scanf(" %c", &ans);
		if (ans == '0')
			continue;
		system("cls");

		// 遊戲開始
		while (1)
		{
			// 初始化排組、玩家及洗牌
			initializeDeck(deck);
			shuffleDeck(deck);
			initializePlayer(&player); initializePlayer(&banker);
			n = 0;

			// 下注階段
			playerBet(&player, user);

			// 第一回合發牌
			for (int i = 0; i < 2; i++)
			{
				deal(&player, &deck);
				deal(&banker, &deck);
			}

			// 印出雙方目前持有的牌
			printf("您目前的牌有: "); printBasicInfo(player, 1);
			printf("莊家目前的牌有: "); printBasicInfo(banker, 0);
			
			

			// 玩家回合
			while (checkHandCardsNumber(player))
			{
				
				char chice;
				// 每次檢查是否爆牌
				if (checkPoint(player) == 0)
					break;
				printf("\n拿牌(1)停牌(0): ");
				scanf(" %c", &chice);
				// 判斷是否正確輸入
				if (chice != '1' && chice != '0')
				{
					printf("輸入錯誤,請重新輸入\n");
					continue;
				}
				else
				{
					// 如果為1則拿牌,並印出目前有的牌
					if (chice == '1')
					{
						deal(&player, &deck);
						printf("您目前的牌有: "); printBasicInfo(player);
					}
					// 如果為0則玩家回合結束
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
	// 莊家第一張牌為暗牌不顯示
	if (isPlayer != 1)
	{
		i = 1;
		printf("? ");
	}
	
	for (;i < player.numCard; i++)
		printf("%s%s ", sufts[player.hand->suitIndex], ranks[player.hand->rankIndex]);
	printf('\n');
	isPlayer ? printf("目前有%d張牌\n", player.numCard) : NULL;
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
		printf("請下注(目前持有金額為%d): ", user.money);
		scanf("%d", &(player->bet));
		// 輸入金額不符合則重新輸入
		if (player->bet > user.money || player->bet <= 0)
			printf("輸入金額錯誤,請重新輸入\n");
		else
			break;
	}
}

int checkPoint(Player player)
{
	int count = 0;
	player.point = 0;
	// 計算目前點數
	for (int i = 0; i < player.numCard; i++)
	{
		if (player.hand->rankIndex == 0)
			count++;
		player.point += player.hand->value;
	}
	while(count)
	{
		// 若A為11時點數超過21,則A為1
		if (player.point > 21)
		{
			player.point -= 10;
			count--;
		}
		// 若點數沒超過21則退出檢查
		else
			break;
	}

	// 檢查是否爆牌
	if (player.point > 21)
		return 0;
	else if (player.point < 21)
		return 1;
	else
		return 2;
}
