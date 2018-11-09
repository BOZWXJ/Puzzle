# coding: utf-8

import sys
import codecs

sys.stdin = codecs.getreader('cp932')(sys.stdin)
sys.stdout = codecs.getwriter('cp932')(sys.stdout)

CARD_MAX = 7
card = []
board = []
count = 0


def main():
    i = 0
    while i < CARD_MAX:
        i = i + 1
        card.append(i)
        board.append(0)
        board.append(0)
    print(str(board), str(card))

    search(1)


def search(z):
    global count
    for i in card:
        if (i != 0):
            # カード未使用
            pos = nextPos()
            if (pos != -1):
                if (setCard(i, pos)):
                    if (nextPos() == -1):
                        count = count + 1
                        print(str(count).rjust(2), board)
                    else:
                        search(z + 1)
                    rsetCard(i, pos)


def nextPos():
    i = 0
    while board[i] != 0:
        i = i + 1
        if (len(board) <= i):
            return -1
    return i


def setCard(num, pos):
    p1 = pos
    p2 = pos + num + 1
    if (len(board) > p2 and board[p1] == 0 and board[p2] == 0):
        card[num - 1] = 0
        board[p1] = num
        board[p2] = num
        return True
    else:
        return False


def rsetCard(num, pos):
    p1 = pos
    p2 = pos + num + 1

    card[num - 1] = num
    board[p1] = 0
    board[p2] = 0


main()
